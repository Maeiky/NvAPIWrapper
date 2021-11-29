﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Management;
using App;

namespace App {
    public class LauchTool {


		public bool bReturnError = false;
		public bool bReturnBoth = false;

		public bool bExeLauch = false;
		public bool bExeLauched = false;
		public bool bStopAll = false;
		public bool bHasError = false;

		public string sExePath = "";
		public string sExeName = "";
		public string sArg = "";
		public string sWorkPath = "";
		public bool bOutput  = true;
		public string sSourceFile  = "";
		public string sTarget  = "";

		public bool bExterneLauch = false;
		public bool bRedirectOutput = true;

		public string sResult ="";
		public	string sError ="";

		public   delegate void dIExit(LauchTool _oTool);
		public   delegate void dIOut(LauchTool _oTool, string _sOut);
		public   delegate void dIError(LauchTool _oTool, string _sOut);

		public   dIExit dExit = null; 
		public   dIOut dOut = null; 
		public   dIError dError = null; 


		public bool bDontKill;
		public bool UseShellExecute = false;
		public  Process ExeProcess = null;


		public Object oCustom = null;

		public bool bRunInThread = true;
		public ProcessStartInfo processStartInfo = null;
		
		public bool bWaitEndForOutput  = false;

        public  LauchTool() {
        }


        public static List<LauchTool> aLauchList = new List<LauchTool>();
        public static bool bListModified = true;


        public string fLauchExe(string _sExePath, string _sArg, string _sSourceFile = "", string _sTarget= "", bool _bDontKill = false) {
			sTarget =  _sTarget;
			sSourceFile = _sSourceFile;
			sArg = _sArg;
			bDontKill = _bDontKill;

			bExeLauch = true;
	
            sExePath = _sExePath;
			if(sWorkPath == "") {
				sWorkPath = _sExePath;
			}
            string _sPath = Path.GetDirectoryName(sExePath);
            if (_sPath != "") {
                sExeName =Path.GetFileName(_sPath);
            }else {
                sExeName = sExePath;
            }


			if(bRunInThread) {
				BackgroundWorker bw = new BackgroundWorker();

				bw.DoWork += new DoWorkEventHandler(
				delegate(object o, DoWorkEventArgs args) {
					fLauch();
				});
				bw.RunWorkerAsync();
			}else {
				return fLauch();
			}
			return "";
				
        }

		
       private  string fLauch() {
				
				string _sResult ="";
				string _sError ="";
            /*
				if(!File.Exists(sExePath)){
					Output.TraceError("Unable to lauch: " + sExePath);
				}*/

					string _sPath = sExePath;
					if(_sPath.IndexOf("..") != -1) {
						_sPath = Path.GetFullPath( sExePath);
					}
				
                     processStartInfo = new ProcessStartInfo( _sPath, sArg);
                    processStartInfo.UseShellExecute = UseShellExecute;

//bWaitEndForOutput = true;



                   ExeProcess = new Process();
               //    ExeProcess.EnableRaisingEvents = true;
               //    ExeProcess.Exited += new EventHandler(fExited);
                   

                     if(bOutput) {
         
						if(!bExterneLauch){
								//processStartInfo.UseShellExecute = false;
							processStartInfo.UseShellExecute = false;
							 processStartInfo.CreateNoWindow = bHidden;
                   // processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
						//		processStartInfo.CreateNoWindow = true;
							processStartInfo.RedirectStandardOutput = bRedirectOutput;
							processStartInfo.RedirectStandardError = true;
							processStartInfo.RedirectStandardInput = true;
						}

/*
						if(!bWaitEndForOutput) {
							ExeProcess.OutputDataReceived += (sender, e) => {
									if (e.Data != null)  {
										fAppOutput(this, e.Data);
									}
								};
							ExeProcess.ErrorDataReceived += (sender, e) => {
								if (e.Data != null)  {
									fAppError(this, e.Data);
								}
							};
						}*/
                    }

                    ExeProcess.StartInfo = processStartInfo;
					 processStartInfo.WorkingDirectory = Path.GetDirectoryName(sWorkPath); 

                    bool processStarted = false;

                    if (bStopAll) {
                		bExeLauched = true;
                        bExeLauch = false;
                        return "";
                    }

                    try {
                        if (bHasError){
                           return "";
                        }

              // Log.print("--------Launch: " +   sExePath + "  " + processStartInfo.Arguments  );


                try{
                    fAddThisToList();
                    processStarted = ExeProcess.Start();
                } catch (Exception e) {
                    Log.print("Unable to lauch: " + sExePath + " ["  + sWorkPath + "] : " + e.Message);
                }
        

                   
						bExeLauched = true;

		
			if(!bExterneLauch && !bWaitEndForOutput) {
				ProcessOutputHandler outputHandler = new ProcessOutputHandler(this);
                   
				if(bRedirectOutput){
					Thread stdOutReader = new Thread(new ThreadStart(outputHandler.ReadStdOut));
					stdOutReader.Start();
				}

				Thread stdErrReader = new Thread(new ThreadStart(outputHandler.ReadStdErr));
				stdErrReader.Start();
//stdOutReader.Priority = ThreadPriority.AboveNormal;
				ExeProcess.WaitForExit(); //Stop here
			}


					//	if(bWaitEndForOutput) {
							if(bRedirectOutput){
								_sResult +=  ExeProcess.StandardOutput.ReadToEnd();
							}
							_sError +=  ExeProcess.StandardError.ReadToEnd();
						
							if(bWaitEndForOutput) { ExeProcess.WaitForExit();}
							
							if(bRedirectOutput && dOut != null && _sResult != ""){
								dOut(this, _sResult);
							}
							if(dError != null  && _sError != ""){
								dError(this, _sError);
							}
/*
						}else {
							 ExeProcess.WaitForExit();
							_sResult +=  ExeProcess.StandardOutput.ReadToEnd();
							_sError +=  ExeProcess.StandardError.ReadToEnd();
							if(dOut != null && _sResult != ""){
								dOut(this, _sResult);
							}
							if(dError != null  && _sError != ""){
								dError(this, _sError);
							}*/

							

						/*
							while(!ExeProcess.HasExited){
								// if (!ExeProcess.StandardOutput.EndOfStream){
									string _sOut = ExeProcess.StandardOutput.ReadLine();
									if(dOut != null && _sOut != ""){
										dOut(this, _sOut);
									}
								//}
								//if (!ExeProcess.StandardOutput.EndOfStream){
									string _sErr = ExeProcess.StandardError.ReadLine();
									if(dError != null  && _sErr != ""){
										dError(this, _sErr);
									}
								//}
								Thread.Sleep(1);
							}
							_sResult +=  ExeProcess.StandardOutput.ReadToEnd();
							_sError +=  ExeProcess.StandardError.ReadToEnd();
						
							 ExeProcess.WaitForExit();
							if(dOut != null && _sResult != ""){
								dOut(this, _sResult);
							}
							if(dError != null  && _sError != ""){
								dError(this, _sError);
							}*/
							
							/*
							 if(bOutput) {
								 ExeProcess.BeginOutputReadLine();///SLOWWW
								 ExeProcess.BeginErrorReadLine();
							}*/
							
					//	}

	
					     ExeProcess.WaitForExit(); //important for geting last output!	
                        fRemoveThisFromList();
						
						sResult = _sResult;
						sError = _sError;

                	     while(!ExeProcess.HasExited ) {
                                Thread.CurrentThread.Join(1);
                          }
                        if(dExit != null) {
                             dExit(this);
                        }


                        /*
                        if(oForm != null) {
                            oForm.fLauchEnd();
                        }*/

                    }  catch (Exception ex){
                         Log.print(ex.Message);
					 }

                    bExeLauch = false;

			if(bReturnBoth){
				return _sResult + "\n" + _sError;
			}else{
				if(!bReturnError){
					return _sResult;
				}else{
					return _sError;
				}
			}
		}


         internal void fAddThisToList() {
            aLauchList.Add(this);
            bListModified = true;
        }
        internal void fRemoveThisFromList() {
            aLauchList.Remove(this);
            bListModified = true;
        }
        



public bool bSanitize = false;
        public bool bHidden = false;

        internal void fEnd() {
		  //  Output.Trace("\f18--Try to Close--");
			bStopAll = true;

            if(dExit != null){ dExit(this);};
          
            while(!ExeProcess.HasExited  ) {
                Thread.CurrentThread.Join(1);
                }
     

            return;


//////////////////
           }
        


          public static   List<Process>  GetChildProcesses(Process process) {


                List<Process> children = new List<Process>();
			/*
                ManagementObjectSearcher mos = new ManagementObjectSearcher(String.Format("Select * From Win32_Process Where ParentProcessID={0}", process.Id));
                foreach (ManagementObject mo in mos.Get())  {
                    children.Add(Process.GetProcessById(Convert.ToInt32(mo["ProcessID"])));
                }
				*/
                return children;
            }
















    }




public class ProcessOutputHandler
{
    public Process proc { get; set; }
    public string StdOut { get; set; }
    public string StdErr { get; set; }

		public LauchTool oTool;

    /// <summary>  
    /// The constructor requires a reference to the process that will be read.  
    /// The process should have .RedirectStandardOutput and .RedirectStandardError set to true.  
    /// </summary>  
    /// <param name="process">The process that will have its output read by this class.</param>  
    public ProcessOutputHandler(LauchTool _oTool )
    {
		oTool = _oTool;
        proc = _oTool.ExeProcess;

        StdErr = "";
        StdOut = "";
   //     Debug.Assert(proc.StartInfo.RedirectStandardError, "RedirectStandardError must be true to use ProcessOutputHandler.");
  //      Debug.Assert(proc.StartInfo.RedirectStandardOutput, "RedirectStandardOut must be true to use ProcessOutputHandler.");
    }

 
    public void ReadStdErr() {
   
        string _sLine;
		if(oTool.dError != null){
                try { 
			while (!proc.HasExited){
				_sLine = proc.StandardError.ReadLine();
				if (_sLine != ""){
						oTool.dError(oTool, _sLine);
				}else{
					Thread.CurrentThread.Join(1);
				}
			}
                } catch(Exception e) { Log.print("Error: " + e.Message);}
		}
    }
    public void ReadStdOut() {
    
        string _sLine;
		if(oTool.dOut != null){
                           try { 
			while (!proc.HasExited){

				_sLine = proc.StandardOutput.ReadLine();
		
				if (_sLine != ""){
						oTool.dOut(oTool, _sLine);
				}else{
					Thread.Sleep(1);
				}
			}
              } catch(Exception e) { Log.print("Error: " + e.Message);}
		}
    }


}




















}