using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSV
{
	
	/// <summary>
	/// The row.
	/// 行,橫的
	/// </summary>
	public string[] Row;

	/// <summary>
	/// The column.
	/// 列,直的
	/// </summary>
	public string[] Column;

	//csv[c,r]
	public string[,] csv;

	public string text{
		get{
			return ToString();
		}
	}

	public CSV(){
		csv = new string[0,0];
		Cache();
	}

	public CSV(int column,int row){
		csv = new string[column,row];
		Cache();
	}

	public CSV(string file){		

		csv = CSV.ParseToArray(file);

		this.Row = new string[csv.GetLength(0)];
		for(int i = 0 ; i < this.Row.Length;i++){
			this.Row[i] = csv[i,0];
		}

		this.Column= new string[csv.GetLength(1)];
		for(int i = 0 ; i < this.Column.Length;i++){
			this.Column[i] = csv[0,i];
		}

	}

	public void AddRow(string arg){
		var oldCsv = csv;
		string[,] newCsv = new string[Row.Length + 1,Column.Length];
		for(int r = 0 ; r <oldCsv.GetLength(1);r++){
			for(int c = 0 ; c<oldCsv.GetLength(0);c++){				
				newCsv[c,r] = oldCsv[c,r];
			}
		}
		newCsv[Row.Length,0] = arg;
		csv = newCsv;
		Cache();
	}

	public void AddColumn(string arg){
		var oldCsv = csv;
		string[,] newCsv = new string[Row.Length,Column.Length + 1];
		for(int r = 0 ; r <oldCsv.GetLength(1);r++){
			for(int c = 0 ; c<oldCsv.GetLength(0);c++){				
				newCsv[c,r] = oldCsv[c,r];
			}
		}
		newCsv[0,Column.Length] = arg;
		csv = newCsv;
		Cache();
	}

	void Cache(){
		this.Row = new string[csv.GetLength(0)];
		for(int i = 0 ; i < this.Row.Length;i++){
			this.Row[i] = csv[i,0];
		}

		this.Column= new string[csv.GetLength(1)];
		for(int i = 0 ; i < this.Column.Length;i++){
			this.Column[i] = csv[0,i];
		}
	}

	public void SetValue(string row , string column , string Value){
		if(!this.HasRow(row)){
			Debug.LogError("CSV : row is not in this csv file."+"Row:" + row+ ",Column:" + column);
			return ;
		}else if(!this.HasColumn(column)){
			Debug.LogError("CSV : column is not in this csv file."+"Row:" + row + ",Column:" + column);
			return ;
		}

		int r = Array.IndexOf( this.Row,row);
		int c = Array.IndexOf( this.Column,column);

		csv[r,c] = Value;
	}

	public string GetValue(string row , string column){
		if(!this.HasRow(row)){
			Debug.LogError("CSV : row is not in this csv file."+"Row:" + row+ ",Column:" + column);
			return null;
		}else if(!this.HasColumn(column)){
			Debug.LogError("CSV : column is not in this csv file."+"Row:" + row + ",Column:" + column);
			return null;
		}

		string Value = null;
//		string[,] scv  = this.ToArray();

		int r = Array.IndexOf( this.Row,row);
		int c = Array.IndexOf( this.Column,column);

		Value = csv[r,c];

		return Value;
	}

	public string GetValue(int row , int column){
		if(!this.HasRow(row)){
			Debug.LogError("CSV : row is not in this csv file.");
			return null;
		}else if(!this.HasColumn(column)){
			Debug.LogError("CSV : column is not in this csv file.");
			return null;
		}

		string Value = null;

//		string[,] scv  = this.ToArray();

		Value = csv[row,column];

		return Value;
	}

	public bool HasRow(string row){

		if(Array.IndexOf( this.Row,row) < 0){

			return false;
		}

		return true;
	}

	public bool HasColumn(string column){
//		if(this.Column == null){
//			Debug.Log("col is null");
//			return false;
//		}
			
		if(Array.IndexOf( this.Column,column) < 0){

			return false;
		}

		return true;
	}

	public bool HasRow(int rowNumber){
		if(rowNumber >= this.Row.Length){
			return false;
		}
		return true;
	}

	public bool HasColumn(int columnNumber){
		if(columnNumber >= this.Column.Length){
			return false;
		}
		return true;
	}

	public override string ToString ()
	{

		string build = "";

		for(int r = 0 ; r <csv.GetLength(1);r++){
			for(int c = 0 ; c<csv.GetLength(0);c++){
				//values[c,r] = line_r[c].Trim('\"');
				build += csv[c,r];
				if(c != csv.GetLength(0)-1)
					build += ",";
			}
			if(r != csv.GetLength(1)-1)
				build += "\n";
		}


		return build;
	}

	public string[,] ToArray(){

		return CSV.ParseToArray(this);

	}

	public static string[,] ParseToArray(CSV csv){
		return CSV.ParseToArray(csv.text);
	}

	public static string[,] ParseToArray(string csv){

		string _csv = csv.Replace('\n','\r');

		string[] lines = _csv.Split(new char[]{'\r'},System.StringSplitOptions.RemoveEmptyEntries);

		//	int num_rows = lines.Length;
		//		int num_cols = lines[0].Split(',').Length;

		//		string[,] values = new string[num_rows,num_cols];


		int num_rows = lines.Length; 
		int num_cols =lines[0].Split(',').Length;

		//Debug.Log("num_rows : " + num_rows + ",num_cols : " + num_cols);

		string[,] values = new string[num_cols,num_rows];

		for(int r = 0 ; r <num_rows;r++){

			string pattern = ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))";   
			Regex re = new Regex(pattern);   
			string[] line_r = re.Split(lines[r]);

			//string[] line_r = lines[r].Split(',');

			for(int c = 0 ; c<num_cols;c++){
				values[c,r] = line_r[c].Trim('\"');
			}

		}

		return values;
	}



}