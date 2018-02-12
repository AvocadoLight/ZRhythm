package com.burningxempires.tool;

import android.app.AlertDialog;
//import android.support.v7.app.AlertDialog;
import android.content.DialogInterface;
import android.util.Log;
import android.widget.Toast;

import com.unity3d.player.UnityPlayer;

import java.lang.reflect.Field;

/**
 * Created by admin on 2017/4/23.
 */

@SuppressWarnings("unused")
public class AndoirdTool {

    public static String ReceiverName;
    public static String MethodName;

    public static void SetListener(String receiverName,String methodName){
        ReceiverName = receiverName;
        MethodName = methodName;
    }

    @SuppressWarnings("unused")
    public static void makeToast(String text){
        Toast.   makeText(UnityPlayer.currentActivity,text, Toast.LENGTH_SHORT).show();
    }


    @SuppressWarnings("unused")
    public static void showAlertDialogBox(String title,String message){
        AlertDialog.Builder dialog = new AlertDialog.Builder(UnityPlayer.currentActivity);
        dialog.setTitle(title);
        dialog.setMessage(message);
        dialog.show();
    }

    public static void showAlertDialogBox(String title,String message,final String... buttons){
        int btnCount = buttons.length;
        AlertDialog.Builder dialog = new AlertDialog.Builder(UnityPlayer.currentActivity);
        dialog.setTitle(title);
        dialog.setMessage(message);
        DialogInterface.OnClickListener onClickListener = new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                Callback(which + "");
            }
        };
        if(btnCount>=1) {//中
            dialog.setNeutralButton(buttons[0],onClickListener);
        }
        if(btnCount>=2) {//左
            dialog.setPositiveButton(buttons[1],onClickListener);
        }
        if(btnCount>=3) {//右
            dialog.setNegativeButton(buttons[2],onClickListener);
        }
        dialog.show();
    }

    public static int getTheme(){
        int theme = UnityPlayer.currentActivity.getResources().getIdentifier(
                "Theme.Material",
                "style",
                UnityPlayer.currentActivity.getPackageName()
        );
        return theme;
    }

    public static void setTheme(){
        UnityPlayer.currentActivity.setTheme(getTheme());
//        UnityPlayer.currentActivity.setTheme(
//                getResourceIdByName(
//                        UnityPlayer.currentActivity.getPackageName(),
//                        "style",
//                        "AppTheme"
//                )
//        );
        //UnityPlayer.currentActivity.setTheme(R.style.AppTheme);
        //UnityPlayer.currentActivity.setTheme(R.style.Theme_AppCompat_DayNight);
    }

    public static void setTheme(int id){
        UnityPlayer.currentActivity.setTheme(id);
    }

    public static  void printRes(){
        final android.R.style styleResources = new android.R.style();
        final Class<android.R.style> c = android.R.style.class;
        final Field[] fields = c.getDeclaredFields();
        int rId;
        for (Field field : fields) {
            try {
                rId = field.getInt(styleResources);
                Log.i("ZRhythm",rId+":--//");
                String ss = UnityPlayer.currentActivity.getResources().getString(rId);
                Log.i("ZRhythm",rId+":"+ss);
                //TODO do something
            } catch (Exception e) {
                continue;
            }
        }
    }

    public static void Callback(String message){
        UnityPlayer.UnitySendMessage(ReceiverName, MethodName, message);
    }

    /**
        *http://stackoverflow.com/questions/1995004/packaging-android-resource-files-within-a-distributable-jar-file
        * */
    public static int getResourceIdByName(String packageName, String className, String name) {
        Class r = null;
        int id = 0;
        try {
            r = Class.forName(packageName + ".R");

            Class[] classes = r.getClasses();
            Class desireClass = null;

            for (int i = 0; i < classes.length; i++) {
                if (classes[i].getName().split("\\$")[1].equals(className)) {
                    desireClass = classes[i];

                    break;
                }
            }

            if (desireClass != null) {
                id = desireClass.getField(name).getInt(desireClass);
            }

        } catch (ClassNotFoundException e) {
            e.printStackTrace();
        } catch (IllegalArgumentException e) {
            e.printStackTrace();
        } catch (SecurityException e) {
            e.printStackTrace();
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        } catch (NoSuchFieldException e) {
            e.printStackTrace();
        }

        return id;
    }

    /**
        * http://ericbaner.iteye.com/blog/1849197
        */
//    public static int getId(Context paramContext, String paramString1, String paramString2)
//    {
//        try
//        {
//            Class localClass = Class.forName(paramContext.getPackageName() + ".R$" + paramString1);
//            Field localField = localClass.getField(paramString2);
//            int i = Integer.parseInt(localField.get(localField.getName()).toString());
//            return i;
//        } catch (Exception localException)
//        {
//            Log.e("getIdByReflection error", localException.getMessage());
//        }
//
//        return 0;
//    }
//    public static int getLayoutResIDByName(Context context, String name) {
//        return context.getResources().getIdentifier(name, "layout",
//                context.getPackageName());
//    }
//
//    public static int getIdResIDByName(Context context, String name) {
//        return context.getResources().getIdentifier(name, "id",
//                context.getPackageName());
//    }
//
//    public static int getStringResIDByName(Context context, String name) {
//        return context.getResources().getIdentifier(name, "string",
//                context.getPackageName());
//    }
//
//    public static int getDrawableResIDByName(Context context, String name) {
//        return context.getResources().getIdentifier(name, "drawable",
//                context.getPackageName());
//    }
//
//    public static int getRawResIDByName(Context context, String name) {
//        return context.getResources().getIdentifier(name, "raw",
//                context.getPackageName());
//    }
}
