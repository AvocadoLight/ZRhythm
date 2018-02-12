package com.burningxempires.tool;

import android.content.ComponentName;
import android.content.ContentUris;
import android.content.Context;
import android.content.res.Configuration;
import android.graphics.PixelFormat;
import android.net.Uri;
import android.text.TextUtils;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.Window;
import android.view.WindowManager;
import android.annotation.TargetApi;
import android.app.Activity;
import android.content.Intent;
import android.database.Cursor;
import android.os.Build;
import android.os.Bundle;
import android.os.Environment;
import android.provider.DocumentsContract;
import android.provider.MediaStore;
import android.util.Log;
import android.webkit.MimeTypeMap;
import android.support.v4.provider.DocumentFile;

import com.unity3d.player.UnityPlayer;

/**
 * Created by admin on 2017/4/22.
 */

public class Gallery extends Activity {
    private static final int SDKVER_KITKAT = 19;
    private static final int REQUEST_GALLERY_KITKAT_ABOVE = 0;
    private static final int REQUEST_GALLERY_JELLYBEAN_BELOW = 1;
    private static final int REQUEST_CODE_OPEN_DIRECTORY = 3;

    public static final String AUDIO_TYPE = "audio/*";
    public static final String IMAGE_TYPE = "image/*";
    //http://www.netmite.com/android/mydroid/frameworks/base/core/java/android/webkit/MimeTypeMap.java
    public static final String[] SUPPORT_AUDIO_TYPE = {"audio/mpeg","audio/mp3","audio/x-mpeg3","audio/x-mpeg-3","audio/x-wav","audio/mpeg3","audio/wav"};
    public static final String[] SUPPORT_IMAGE_TYPE = {"image/pjpeg","image/jpeg","image/png"};
//    public static final String SUPPORT_AUDIO_TYPE = "audio/mpeg3;audio/wav";
//    public static final String SUPPORT_IMAGE_TYPE = "image/jpeg;image/png";
    public static final String DEFAULT_TYPE = "*/*";
    public static final String STRING_NULL = "null";

    public static String ReceiverName;
    public static String MethodName;

    public static String resultPath = null;
    public static String galleryType = "*/*";
    protected UnityPlayer mUnityPlayer; // don't change the name of this variable; referenced from native code

    @SuppressWarnings("unused")
    public static void openFolderGallery(String receiver,String method){
        ReceiverName = receiver;
        MethodName = method;
        //Intent i = new Intent(Intent.ACTION_OPEN_DOCUMENT_TREE);
        //i.addCategory(Intent.CATEGORY_DEFAULT);
       // startActivityForResult(Intent.createChooser(i, "Choose directory"), 9999);

        UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
            public void run() {

                Intent intent = new Intent(Intent.ACTION_OPEN_DOCUMENT_TREE);
                intent.addCategory(Intent.CATEGORY_DEFAULT);
                UnityPlayer.currentActivity.startActivityForResult(Intent.createChooser(intent, "Choose directory"), REQUEST_CODE_OPEN_DIRECTORY);
            }
        });
    }

    @SuppressWarnings("unused")
    public static void openAudioGallery(String receiver,String method) {
        galleryType = AUDIO_TYPE;
        openGallery(receiver,method);
    }

    @SuppressWarnings("unused")
    public static void openImageGallery(String receiver, String method) {
        galleryType = IMAGE_TYPE;
        openGallery(receiver,method);
    }

    @SuppressWarnings("unused")
    public static void openGallery(String mimetype,String receiver,String method) {
        galleryType = mimetype;
        openGallery(receiver,method);
    }

    @SuppressWarnings("unused")
    public static void openGallery(String receiver,String method) {

        ReceiverName = receiver;
        MethodName = method;

        Log.i("ZRhythm","openGallery");
        Log.i("ZRhythm", "mp3 mime:"+MimeTypeMap.getSingleton().getMimeTypeFromExtension("mp3"));
        Log.i("ZRhythm", "wav mime:"+MimeTypeMap.getSingleton().getMimeTypeFromExtension("wav"));
        Log.i("ZRhythm", "png mime:"+MimeTypeMap.getSingleton().getMimeTypeFromExtension("png"));
        Log.i("ZRhythm", "jpg mime:"+MimeTypeMap.getSingleton().getMimeTypeFromExtension("jpg"));
        Log.i("ZRhythm", "rct mime:"+MimeTypeMap.getSingleton().getMimeTypeFromExtension("rct"));
        UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
            public void run() {
                Log.i("ZRhythm","openGallery-runOnUiThread");

                Intent intent = new Intent();                                 //plant A

                if(!TextUtils.isEmpty(galleryType)){
                    Log.i("ZRhythm","openGallery-Intent");
                    intent.setType(galleryType);
                }else{
                    intent.setType(DEFAULT_TYPE);
                }

                if (Build.VERSION.SDK_INT < SDKVER_KITKAT) {
                    intent.setAction(Intent.ACTION_GET_CONTENT);
                    intent.putExtra(Intent.EXTRA_LOCAL_ONLY,true);
                    if(galleryType == AUDIO_TYPE){
                        intent.putExtra(Intent.EXTRA_MIME_TYPES,SUPPORT_AUDIO_TYPE);
                    }else if(galleryType == IMAGE_TYPE){
                        intent.putExtra(Intent.EXTRA_MIME_TYPES,SUPPORT_IMAGE_TYPE);
                    }
                    UnityPlayer.currentActivity.startActivityForResult(intent, REQUEST_GALLERY_JELLYBEAN_BELOW);
                } else {
                    intent.setAction(Intent.ACTION_OPEN_DOCUMENT);
                    intent.putExtra(Intent.EXTRA_LOCAL_ONLY,true);
                    if(galleryType == AUDIO_TYPE){
                        intent.putExtra(Intent.EXTRA_MIME_TYPES,SUPPORT_AUDIO_TYPE);
                    }else if(galleryType == IMAGE_TYPE){
                        intent.putExtra(Intent.EXTRA_MIME_TYPES,SUPPORT_IMAGE_TYPE);
                    }
                    intent.addCategory(Intent.CATEGORY_OPENABLE);
                    UnityPlayer.currentActivity.startActivityForResult(intent, REQUEST_GALLERY_KITKAT_ABOVE);
                }
            }
        });
    }
    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode,resultCode,data);

        Log.i("ZRhythm","onActivityResult");
        if(resultCode != RESULT_OK)
        {
            Log.i("ZRhythm","onActivityResult-fucking return!!");
            return;
        }

        switch (requestCode)
        {
            case REQUEST_CODE_OPEN_DIRECTORY:
                Log.d("ZRhythm", "Result URI " + data.getData());
                Uri uri = data.getData();
                //https://stackoverflow.com/questions/29713587/howto-get-the-real-path-with-action-open-document-tree-intent-lollipop-api-21
                Uri doc = DocumentsContract.buildDocumentUriUsingTree(uri,
                        DocumentsContract.getTreeDocumentId(uri));
                resultPath = getPath(Gallery.this,doc);
                SendMessage(resultPath);
                break;

            case REQUEST_GALLERY_JELLYBEAN_BELOW:
                // 選択した画像のパスを取得する.
                String[] strColumns = {MediaStore.Files.FileColumns.DATA };
                if(galleryType == AUDIO_TYPE) {
                    strColumns = new String[]{MediaStore.Audio.Media.DATA};
                }else if(galleryType == IMAGE_TYPE){
                    strColumns = new String[]{MediaStore.Images.Media.DATA};
                }
                Cursor crsCursor = getContentResolver().query(data.getData(), strColumns, null, null, null);
                if(crsCursor.moveToFirst())
                {
                    Log.d("ZRhythm", crsCursor.getString(0));
                }

                resultPath = crsCursor.getString(0);
                SendMessage(resultPath);

                crsCursor.close();
                Log.i("ZRhythm","onActivityResult-case:REQUEST_GALLERY_JELLYBEAN_BELOW->" + resultPath);
                break;
            case REQUEST_GALLERY_KITKAT_ABOVE:
                this.GetSelectedItemPath(data);
                break;
            default:
                Log.i("ZRhythm","onActivityResult-default WTF!!!" + requestCode);
                break;
        }
    }

    @TargetApi(SDKVER_KITKAT)
    private void GetSelectedItemPath(Intent data)
    {

        //below code is from -> http://givemepass.blogspot.tw/2016/09/imagepickercreatechooser.html
        Uri uri = data.getData();
        resultPath = getPath(Gallery.this,uri);

        //Toast.makeText(Gallery.this, "resultPath:" + resultPath, Toast.LENGTH_SHORT).show();
        Log.i("ZRhythm","onActivityResult-case:REQUEST_GALLERY_KITKAT_ABOVE->"+ resultPath);
        Log.i("ZRhythm","onActivityResult-case:REQUEST_GALLERY_KITKAT_ABOVE->"+ data.getDataString());
        Log.i("ZRhythm","onActivityResult-case:REQUEST_GALLERY_KITKAT_ABOVE->"+ uri.toString());

        UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
            public void run() {
                SendMessage(resultPath);
            }
        });

    }

    private void SendMessage(String message){
        Log.i("ZRhythm","Receiver: " + ReceiverName + " MethodName: " +MethodName+ " SendMessage: "+message);
        if(!TextUtils.isEmpty(message))
            UnityPlayer.UnitySendMessage(ReceiverName, MethodName, message);
        else
            UnityPlayer.UnitySendMessage(ReceiverName, MethodName, STRING_NULL);
    }

    /**
     * Get a file path from a Uri. This will get the the path for Storage Access
     * Framework Documents, as well as the _data field for the MediaStore and
     * other file-based ContentProviders.
     *
     * @param context The context.
     * @param uri The Uri to query.
     * @author paulburke
     */
    @TargetApi(Build.VERSION_CODES.KITKAT)
    public static String getPath(final Context context, final Uri uri) {

        final boolean isKitKat = Build.VERSION.SDK_INT >= Build.VERSION_CODES.KITKAT;

        // DocumentProvider
        if (isKitKat && DocumentsContract.isDocumentUri(context, uri)) {
            // ExternalStorageProvider
            /*if (isExternalStorageDocument(uri)) {

                final String docId = DocumentsContract.getDocumentId(uri);
                final String[] split = docId.split(":");
                final String type = split[0];
                Log.i("ZRhythm","isExternalStorageDocument , type:" + type);
                if ("primary".equalsIgnoreCase(type)) {
                    return Environment.getExternalStorageDirectory() + "/" + split[1];
                }

                // TODO handle non-primary volumes
            }*/
            //below code is form http://stackoverflow.com/questions/33295300/how-to-get-absolute-path-in-android-for-file
            if (isExternalStorageDocument(uri)) {// ExternalStorageProvider
                final String docId = DocumentsContract.getDocumentId(uri);
                final String[] split = docId.split(":");
                final String type = split[0];
                String storageDefinition;
                if("primary".equalsIgnoreCase(type)){
                    return Environment.getExternalStorageDirectory() + "/" + split[1];
                } else {
                    if(Environment.isExternalStorageRemovable()){
                        storageDefinition = "EXTERNAL_STORAGE";
                    } else{
                        storageDefinition = "SECONDARY_STORAGE";
                    }
                    return System.getenv(storageDefinition) + "/" + split[1];
                }
            }
            //upper code is form http://stackoverflow.com/questions/33295300/how-to-get-absolute-path-in-android-for-file
            // DownloadsProvider
            else if (isDownloadsDocument(uri)) {
                Log.i("ZRhythm","isDownloadsDocument");
                final String id = DocumentsContract.getDocumentId(uri);
                final Uri contentUri = ContentUris.withAppendedId(
                        Uri.parse("content://downloads/public_downloads"), Long.valueOf(id));

                return getDataColumn(context, contentUri, null, null);
            }
            // MediaProvider
            else if (isMediaDocument(uri)) {
                Log.i("ZRhythm","isMediaDocument");
                final String docId = DocumentsContract.getDocumentId(uri);
                final String[] split = docId.split(":");
                final String type = split[0];

                Uri contentUri = null;
                if ("image".equals(type)) {
                    contentUri = MediaStore.Images.Media.EXTERNAL_CONTENT_URI;
                } else if ("video".equals(type)) {
                    contentUri = MediaStore.Video.Media.EXTERNAL_CONTENT_URI;
                } else if ("audio".equals(type)) {
                    contentUri = MediaStore.Audio.Media.EXTERNAL_CONTENT_URI;
                }

                final String selection = "_id=?";
                final String[] selectionArgs = new String[] {
                        split[1]
                };

                return getDataColumn(context, contentUri, selection, selectionArgs);
            }
        }
        // MediaStore (and general)
        else if ("content".equalsIgnoreCase(uri.getScheme())) {
            Log.i("ZRhythm","content");
            return getDataColumn(context, uri, null, null);
        }
        // File
        else if ("file".equalsIgnoreCase(uri.getScheme())) {
            Log.i("ZRhythm","file");
            return uri.getPath();
        }
        Log.i("ZRhythm","getpath null");
        return null;
    }

    /**
     * Get the value of the data column for this Uri. This is useful for
     * MediaStore Uris, and other file-based ContentProviders.
     *
     * @param context The context.
     * @param uri The Uri to query.
     * @param selection (Optional) Filter used in the query.
     * @param selectionArgs (Optional) Selection arguments used in the query.
     * @return The value of the _data column, which is typically a file path.
     */
    public static String getDataColumn(Context context, Uri uri, String selection,
                                       String[] selectionArgs) {

        Cursor cursor = null;
        final String column = "_data";
        final String[] projection = {
                column
        };

        try {
            cursor = context.getContentResolver().query(uri, projection, selection, selectionArgs,
                    null);
            if (cursor != null && cursor.moveToFirst()) {
                final int column_index = cursor.getColumnIndexOrThrow(column);
                return cursor.getString(column_index);
            }
        } finally {
            if (cursor != null)
                cursor.close();
        }
        return null;
    }


    /**
     * @param uri The Uri to check.
     * @return Whether the Uri authority is ExternalStorageProvider.
     */
    public static boolean isExternalStorageDocument(Uri uri) {
        return "com.android.externalstorage.documents".equals(uri.getAuthority());
    }

    /**
     * @param uri The Uri to check.
     * @return Whether the Uri authority is DownloadsProvider.
     */
    public static boolean isDownloadsDocument(Uri uri) {
        return "com.android.providers.downloads.documents".equals(uri.getAuthority());
    }

    /**
     * @param uri The Uri to check.
     * @return Whether the Uri authority is MediaProvider.
     */
    public static boolean isMediaDocument(Uri uri) {
        return "com.android.providers.media.documents".equals(uri.getAuthority());
    }

    // --- Copied from UnityPlayerActivity.java ---

    // Setup activity layout
    @Override protected void onCreate (Bundle savedInstanceState)
    {
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        super.onCreate(savedInstanceState);
        getWindow().setFormat(PixelFormat.RGBX_8888); // <--- This makes xperia play happy

        mUnityPlayer = new UnityPlayer(this);
        if (mUnityPlayer.getSettings ().getBoolean ("hide_status_bar", true))
        {
            setTheme(android.R.style.Theme_NoTitleBar_Fullscreen);
            getWindow ().setFlags (WindowManager.LayoutParams.FLAG_FULLSCREEN,
                    WindowManager.LayoutParams.FLAG_FULLSCREEN);
        }

        setContentView(mUnityPlayer);
        mUnityPlayer.requestFocus();
    }
    // Quit Unity
    @Override protected void onDestroy ()
    {
        mUnityPlayer.quit();
        super.onDestroy();
    }

    // Pause Unity
    @Override protected void onPause()
    {
        super.onPause();
        mUnityPlayer.pause();
    }

    // Resume Unity
    @Override protected void onResume()
    {
        super.onResume();
        mUnityPlayer.resume();
    }

    // This ensures the layout will be correct.
    @Override public void onConfigurationChanged(Configuration newConfig)
    {
        super.onConfigurationChanged(newConfig);
        mUnityPlayer.configurationChanged(newConfig);
    }

    // Notify Unity of the focus change.
    @Override public void onWindowFocusChanged(boolean hasFocus)
    {
        super.onWindowFocusChanged(hasFocus);
        mUnityPlayer.windowFocusChanged(hasFocus);
    }

    // For some reason the multiple keyevent type is not supported by the ndk.
    // Force event injection by overriding dispatchKeyEvent().
    @Override public boolean dispatchKeyEvent(KeyEvent event)
    {
        if (event.getAction() == KeyEvent.ACTION_MULTIPLE)
            return mUnityPlayer.injectEvent(event);
        return super.dispatchKeyEvent(event);
    }

    // Pass any events not handled by (unfocused) views straight to UnityPlayer
    @Override public boolean onKeyUp(int keyCode, KeyEvent event)     { return mUnityPlayer.injectEvent(event); }
    @Override public boolean onKeyDown(int keyCode, KeyEvent event)   { return mUnityPlayer.injectEvent(event); }
    @Override public boolean onTouchEvent(MotionEvent event)          { return mUnityPlayer.injectEvent(event); }
    /*API12*/ public boolean onGenericMotionEvent(MotionEvent event)  { return mUnityPlayer.injectEvent(event); }

    // ------
}
