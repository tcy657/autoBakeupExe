rem @echo  off

rem *************************  Var   
    ::--------------------------------------------------------  
    ::-- Function section starts below here  
    ::-- %~1, D:\11\pscp_1\pscp.exe
    ::-- %~2, 10.190.16.2
    ::-- %~3, /home/scu/ZEBOS.CFG
    ::--------------------------------------------------------

    set "pscp=%~1"
    set "ip1=%~2"
    set "DirPC=%~3%"
    set "FileInDev=%~4%" 
    rem echo %pscp%,  %ip1%,  %DirPC%, %FileInDev% &pause
rem *************************  Var End   

rem *************************  Delay   
    setlocal enabledelayedexpansion
rem *************************  Delay End   

rem *************************  Result.log  
rem    rem >4M, delete Result.log
rem    FOR /f "delims=" %%i in ("./") do (
rem    IF %%~zi gtr 4194304 ( DEL /F /A /Q Result.log) 
rem    echo %time%>>Result.log.txt
rem *************************  Result.log End   

rem *************************  1th  
   set "NE=%DirPC%\%ip1%\"
   call:myDosFunc %pscp%,  %ip1%, %FileInDev%,   %NE%
   rem echo.get-1th &pause 
rem *************************  1th  End 

rem *************************  2th    
  set "NE_TMP=%DirPC%\%ip1%_TMP\"
  call:myDosFunc %pscp%,  %ip1%, %FileInDev%,  %NE_TMP%
  rem   echo.get-2th &pause  
  goto:eof

rem *************************  2th End 
 
rem *************************  Function  
    ::--------------------------------------------------------
    ::-- Function section starts below here  
    ::-- %~1, D:\11\pscp_1\pscp.exe
    ::-- %~2, 10.190.16.2
    ::-- %~3, /home/scu/ZEBOS.CFG
    ::--------------------------------------------------------
    :myDosFunc   
    ::echo y|D:\11\pscp_1\pscp.exe -v -p -l root -pw "root"  root@10.190.16.2:/home/scu/ZEBOS.CFG "%NE_TMP%ZEBOS_R1.CFG"

    echo. y|%~1 -v -p -l root -pw "root"  root@%~2:%~3 %~4  
    if %errorlevel% == 0 (echo Result: %~2:%~3 Succeeded!!! >>./Result.log) else (echo Result: %~2:%~3 Failed!!! >>./Result.log  & exit /B 130)
    goto:eof   

rem *************************  Function End                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         