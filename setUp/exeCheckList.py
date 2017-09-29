# -*- coding:gb2312 -*-
import time
import logging
import shutil
import random
import os
import time
import sys
import ctypes
#from pywinauto import *
from ctypes import *

import pythoncom
#get username
import getpass

#get pid, 2016/8/9 18:45:00
import psutil
import re
import string

#get new testLog, 2016/8/10 14:51:29
import datetime

#打印异常，2016/10/18
import traceback
import tkMessageBox

#~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
#获取脚本文件的当前路径，类D:\FHATP\
def cur_file_dir():
  try:
     #获取脚本路径
     path = sys.path[0]
     path = path + '\\'
     #print(path +'123')
     #判断为脚本文件还是py2exe编译后的文件;
     #1) py脚本文件，则返回脚本的目录,如D:\FHATP
     #2) py2exe编译后的exe，则返回EXE文件路径,如D:\FHATP\xx.exe
     if os.path.isdir(path):
         return path
     elif os.path.isfile(path):
         return os.path.dirname(path)
  except Exception, e:
    exstr = traceback.format_exc()
    print( "cur_file_dir(), error!" + '\n' + exstr)
    pass  #空语句 do nothing 
    return 0 #不存在 
#~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
#def main():
if __name__ == "__main__":
 try:  
  #检查4，工具根目录下完整性清单
  arrOtnmListAll =["DiffieHellman.dll",           
  "fhBK.ini",
  "IP.ini",
  "MySql.Data.dll",
  "Org.Mentalis.Security.dll",
  "Tamir.SharpSSH.dll",
  "自动备份助手.exe",
  "自动备份助手使用说明.pdf"]  

  #获取根目录工作路径
  rootPath = cur_file_dir() #类似D:/jenkins/workspace/xx/
  otnm_outputPath =rootPath +"bin_cytao\\"
  print('根目录-' + rootPath)
  
  #拷贝“自动备份助手.exe”
  print("1-拷贝自动备份助手.exe")
  if not os.path.exists(rootPath +"..\\自动备份\\bin\\Debug\\自动备份助手.exe"):
       print("1-自动备份助手.exe不存在") 
       tkMessageBox.showinfo(title=u'工具检查提示!', message=u"关键文件自动备份助手.exe缺失，程序退出！")
       sys.exit(1)  #退出
  else:
       if os.path.exists(rootPath +".\\bin_cytao\\自动备份助手.exe"): #删除旧文件
          os.remove(rootPath +".\\bin_cytao\\自动备份助手.exe")
       shutil.copy(rootPath +"..\\自动备份\\bin\\Debug\\自动备份助手.exe", rootPath +".\\bin_cytao\\自动备份助手.exe")
  
  print("2-检查工具的完整性")
  sameFlag=True  #文件是否完整，是-True; 否-false
  for fileComp in arrOtnmListAll: #检查文件是否存在
       fileCompPath =otnm_outputPath +fileComp; 
       if not os.path.exists(fileCompPath):   #arrOtnmListAll(i)是否存在
           sameFlag=False #文件不存在
           break

  if False == sameFlag: #文件缺失，程序退出！
       print("3-关键文件缺失，程序退出！--" +fileCompPath) 
       #tkMessageBox.showinfo(title=u'工具检查提示!', message=u"关键文件缺失，程序退出！--" +fileCompPath)
       sys.exit(1)  #退出
  print("3-完整性检查通过，可以打包!")
  
  #a=input("输入任意内容退出！")
  print("5秒后程序退出！")
  time.sleep(5)
  sys.exit(1)  #退出
 except Exception, e:
    exstr = traceback.format_exc()
    print( "main(), error!" + '\n' + exstr)
    pass  #空语句 do nothing 