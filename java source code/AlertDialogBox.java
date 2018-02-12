package com.burningxempires.tool;


import android.annotation.TargetApi;
import android.content.DialogInterface;
import android.app.AlertDialog;
//import android.support.v7.app.AlertDialog;
import android.text.Html;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.ScrollView;
import android.widget.TextView;
import android.widget.ToggleButton;

import com.unity3d.player.UnityPlayer;

import org.w3c.dom.Text;

import java.util.Vector;

/**
 * Created by admin on 2017/4/30.
 */

@SuppressWarnings("unused")
public class AlertDialogBox {

    public ICallBack callBack;

    public AlertDialog.Builder m_builder;
    public AlertDialog m_alertdialog;

    public LinearLayout m_layout;

    /*public AlertDialogBox(ICallBack callback,int theme){
        callBack = callback;
        if(theme == -1) {
            theme = UnityPlayer.currentActivity.getResources().getIdentifier(
                    "Theme.AppCompat.Light",
                    "style",
                    UnityPlayer.currentActivity.getPackageName()
            );
        }
        m_builder = new AlertDialog.Builder(UnityPlayer.currentActivity,theme);
    }*/


    public AlertDialogBox(ICallBack callback,boolean useTheme){
        callBack = callback;
        if(useTheme)
            m_builder = new AlertDialog.Builder(UnityPlayer.currentActivity,AndoirdTool.getTheme());
        else
            m_builder = new AlertDialog.Builder(UnityPlayer.currentActivity);

    }

    public AlertDialogBox(String title, String message,ICallBack callback,boolean useTheme){
        callBack = callback;
        if(useTheme)
            m_builder = new AlertDialog.Builder(UnityPlayer.currentActivity,AndoirdTool.getTheme());
        else
            m_builder = new AlertDialog.Builder(UnityPlayer.currentActivity);
        m_builder.setTitle(title);
        m_builder.setMessage(message);
    }

    public LinearLayout getLayout(){
        if(m_layout == null){
            m_layout = new LinearLayout(UnityPlayer.currentActivity);
        }
        return m_layout;
    }

    public void BreakLine(){
        getLayout().setLayoutParams(
                new LinearLayout.LayoutParams(
                        LinearLayout.LayoutParams.MATCH_PARENT,
                        LinearLayout.LayoutParams.WRAP_CONTENT
                )
        );
        getLayout().setOrientation(LinearLayout.VERTICAL);
    }

    public void makeItScroll(){
        ScrollView scrollView = new ScrollView(UnityPlayer.currentActivity);
        scrollView.addView(getLayout());
        m_builder.setView(scrollView);
    }

    public void makeItList(){
        ListView listView = new ListView(UnityPlayer.currentActivity);
        listView.addView(getLayout());
        m_builder.setView(listView);
    }

    public void setLayoutPadding(int left,int right,int top,int bottom){
        getLayout().setPadding(left, right, top, bottom);
        m_builder.setView(getLayout());
    }

    public EditText addInputText(String text){
        EditText input = new EditText(UnityPlayer.currentActivity);
        input.setText(text);
        getLayout().addView(input);
        m_builder.setView(getLayout());
        return input;
    }

    public ToggleButton addToggleButton(String text,boolean checked){
        ToggleButton toggle = new ToggleButton(UnityPlayer.currentActivity);
        toggle.setText(text);
        toggle.setChecked(checked);
        getLayout().addView(toggle);
        m_builder.setView(getLayout());
        return toggle;
    }

    public CheckBox addCheckBox(String text , boolean checked){
        CheckBox checkbox = new CheckBox(UnityPlayer.currentActivity);
        checkbox.setText(text);
        checkbox.setChecked(checked);
        getLayout().addView(checkbox);
        m_builder.setView(getLayout());
        return  checkbox;
    }

    public TextView addTextView(String text){
        TextView textView = new TextView(UnityPlayer.currentActivity);
        textView.setText(text);
        getLayout().addView(textView);
        m_builder.setView(getLayout());
        return textView;
    }

    public void setTitle(String title){
        m_builder.setTitle(title);
    }

    public void setMessage(String message){
        m_builder.setMessage(message);
    }

    public void setButton(int button,String text){
        DialogInterface.OnClickListener onClickListener = new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                onCallBack(which);
            }
        };
        if(button == AlertDialog.BUTTON_NEUTRAL) {//中-3
            m_builder.setNeutralButton(text,onClickListener);
        }
        if(button == AlertDialog.BUTTON_POSITIVE) {//左-1
            m_builder.setPositiveButton(text,onClickListener);
        }
        if(button == AlertDialog.BUTTON_NEGATIVE) {//右-2
            m_builder.setNegativeButton(text,onClickListener);
        }
    }

    public EditText setInputText(String text){
        EditText input = new EditText(UnityPlayer.currentActivity);
        input.setText(text);
        m_builder.setView(input);
        return input;
    }

    public ToggleButton setToggleButton(String text , boolean checked){
        ToggleButton toggle = new ToggleButton(UnityPlayer.currentActivity);
        toggle.setText(text);
        toggle.setChecked(checked);
        m_builder.setView(toggle);
        return  toggle;
    }

    public CheckBox setCheckBox(String text , boolean checked){
        CheckBox checkbox = new CheckBox(UnityPlayer.currentActivity);
        checkbox.setText(text);
        checkbox.setChecked(checked);
        m_builder.setView(checkbox);
        return  checkbox;
    }

    public TextView setTextView(String text,boolean isHtml){
        TextView textview = new CheckBox(UnityPlayer.currentActivity);
        if(isHtml)
            textview.setText(Html.fromHtml(text,Html.FROM_HTML_MODE_LEGACY));
        else
            textview.setText(text);
        m_builder.setView(textview);
        return  textview;
    }

    public int getButtonNeutral(){
        return AlertDialog.BUTTON_NEUTRAL;
    }

    public int getButtonPositive(){
        return AlertDialog.BUTTON_POSITIVE;
    }

    public int getButtonNegative(){
        return AlertDialog.BUTTON_NEGATIVE;
    }

    public void Show(){
        m_alertdialog = m_builder.show();
    }

    public void Cancel(){
        m_alertdialog.cancel();
    }

    public void onCallBack(int which){
        //Unity Callback
        callBack.onCallBack(which);
    }


}
