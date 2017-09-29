rem @echo  off

rem *************************  Var   
    ::--------------------------------------------------------  
    ::-- Function section starts below here  
    ::-- %~1, D:\11\pscp_1\pscp.exe
    ::-- %~2, 10.190.16.2
    ::-- %~3, /home/scu/ZEBOS.CFG
    ::-- %~4, %NE_TMP%ZEBOS_R1.CFG
    ::-- %~5
    ::--------------------------------------------------------
    
    rem get var 
    set "pscp=%~1"
    set "ip1=%~2"
    set "DirPC=%~3"
    set "FileInDev=%~4" 
    rem echo %pscp%,  %ip1%,  %DirPC%, %FileInDev% &pause
rem *************************  Var End   

rem *************************  Delay   
    rem var delay
    setlocal enabledelayedexpansion
    
    rem fc return
    set var_return=0 
    
    rem redo times
    set redoTimes=0 
rem *************************  Delay End   


rem *************************  Result.log  
rem    rem >4M, delete Result.log
rem    FOR /f "delims=" %%i in ("./") do (
rem    IF %%~zi gtr 4194304 ( DEL /F /A /Q Result.log) 
rem    
   echo %time%>>Result.log.txt
rem *************************  Result.log End   

rem *************************  NE is OK?   
rem ping is ok?
ping %ip1% -n 3 -w 2000 >nul
if %ERRORLEVEL% NEQ 0 ( exit /B 129)
rem *************************  NE is OK? End 
 
   :round 
  if "%~4"=="" goto end 
 

rem creat dir
   IF NOT EXIST "%DirPC%" MD "%DirPC%"

rem *************************  1th  
   set "NE=%DirPC%\%ip1%\"
   rem  echo NE="%NE%"&pause  
   rem mkdir "%NE%"
     IF NOT EXIST "%NE%" MD "%NE%"                    
   rem  if not exist "%NE%" echo y|mkdir "%NE%" else echo "EXSIT" &pause
         
   call:myDosFunc %pscp%,  %ip1%, %FileInDev%,   %NE%
   rem echo.get-1th &pause 
rem *************************  1th  End 

rem *************************  2th  
  :ReDo 
  rem 2th
  
  set /a redoTimes =%redoTimes%+1
  set time1=%redoTimes%
  
  IF %time1% GEQ 3 (
  rd /S /Q "!ip!"
  rd /S /Q "%NE%" 
  rem xTimes failed
  exit /B 130)
  
  set "NE_TMP=%DirPC%\%ip1%_1\"
rem  mkdir ""%NE_TMP%"
   IF NOT EXIST "%NE_TMP%" MD "%NE_TMP%"
rem  if not exist "%NE_TMP%" echo y|mkdir "%NE_TMP%" else echo "_1EXSIT" 
  
     echo.going to execute myDosFunc with different arguments
     call:myDosFunc %pscp%,  %ip1%, %FileInDev%,  %NE_TMP%
    rem    echo.get-2th &pause  


  shift /4
  goto round 

:comp
    call:myDosFc %NE%,  %NE_TMP%, %FileInDev%
    IF %var_return% GEQ 1 (
    rd /S /Q "%NE%" 
    rename "%NE_TMP%" "!ip!" 
    set var_return=0 
    rem echo."diff, get again!" &pause 
    goto ReDo) else (
    rd /S /Q "%NE_TMP%") 
    rem echo."same, OK!" &pause 
    goto:eof

rem *************************  2th End 
 
rem *************************  Function  
    ::--------------------------------------------------------  
    ::-- Function section starts below here  
    ::-- %~1, D:\11\pscp_1\pscp.exe
    ::-- %~2, 10.190.16.2
    ::-- %~3, /home/scu/ZEBOS.CFG
    ::-- %~4, %NE_TMP%ZEBOS_R1.CFG
    ::-- %~5
    ::--------------------------------------------------------  
    :myDosFunc    - here starts my function identified by it's label  
    ::echo y|D:\11\pscp_1\pscp.exe -v -p -l root -pw "root"  root@10.190.16.2:/home/scu/ZEBOS.CFG "%NE_TMP%ZEBOS_R1.CFG"

    echo. y|%~1 -v -p -l root -pw "root"  root@%~2:%~3 %~4  
    if %errorlevel% == 0 (echo Result: %~2:%~3 Succeeded!!! >>./Result.log) else (echo Result: %~2:%~3 Failed!!! >>./Result.log)
    goto:eof                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
)    
    ::--------------------------------------------------------  
    ::-- Function section starts below here  
    ::-- %~1, NE
    ::-- %~2, NE_TMP
    ::-- %~3, ZEBOS.CFG
    ::-- %~4, mac.conf
    ::-- %~5, nmagent_config
    ::--------------------------------------------------------
    :myDosFc    - here starts my function identified by it's label  
    set dir1=%~1
    set dir2=%~2
    for /f "tokens=*" %%i in ("%~3.$") do set "File1=%%~ni"
 
    :loop
    if "%~3" NEQ "" (
    echo.| fc /b %dir1%%File1% %dir2%%File1%
    IF ERRORLEVEL 1 set var_return=1 
    shift /1  
    goto :loop)
    
    ::echo.test-%3 &pause
    goto:eof       
rem *************************  Function End                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         