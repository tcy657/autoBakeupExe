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

#��ӡ�쳣��2016/10/18
import traceback
import tkMessageBox

#~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
#��ȡ�ű��ļ��ĵ�ǰ·������D:\FHATP\
def cur_file_dir():
  try:
     #��ȡ�ű�·��
     path = sys.path[0]
     path = path + '\\'
     #print(path +'123')
     #�ж�Ϊ�ű��ļ�����py2exe�������ļ�;
     #1) py�ű��ļ����򷵻ؽű���Ŀ¼,��D:\FHATP
     #2) py2exe������exe���򷵻�EXE�ļ�·��,��D:\FHATP\xx.exe
     if os.path.isdir(path):
         return path
     elif os.path.isfile(path):
         return os.path.dirname(path)
  except Exception, e:
    exstr = traceback.format_exc()
    print( "cur_file_dir(), error!" + '\n' + exstr)
    pass  #����� do nothing 
    return 0 #������ 
#~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
#def main():
if __name__ == "__main__":
 try:  
  #���4�����߸�Ŀ¼���������嵥
  arrOtnmListAll =["DiffieHellman.dll",           
  "fhBK.ini",
  "IP.ini",
  "MySql.Data.dll",
  "Org.Mentalis.Security.dll",
  "Tamir.SharpSSH.dll",
  "�Զ���������.exe",
  "�Զ���������ʹ��˵��.pdf"]  

  #��ȡ��Ŀ¼����·��
  rootPath = cur_file_dir() #����D:/jenkins/workspace/xx/
  otnm_outputPath =rootPath +"bin_cytao\\"
  print('��Ŀ¼-' + rootPath)
  
  #�������Զ���������.exe��
  print("1-�����Զ���������.exe")
  if not os.path.exists(rootPath +"..\\�Զ�����\\bin\\Debug\\�Զ���������.exe"):
       print("1-�Զ���������.exe������") 
       tkMessageBox.showinfo(title=u'���߼����ʾ!', message=u"�ؼ��ļ��Զ���������.exeȱʧ�������˳���")
       sys.exit(1)  #�˳�
  else:
       if os.path.exists(rootPath +".\\bin_cytao\\�Զ���������.exe"): #ɾ�����ļ�
          os.remove(rootPath +".\\bin_cytao\\�Զ���������.exe")
       shutil.copy(rootPath +"..\\�Զ�����\\bin\\Debug\\�Զ���������.exe", rootPath +".\\bin_cytao\\�Զ���������.exe")
  
  print("2-��鹤�ߵ�������")
  sameFlag=True  #�ļ��Ƿ���������-True; ��-false
  for fileComp in arrOtnmListAll: #����ļ��Ƿ����
       fileCompPath =otnm_outputPath +fileComp; 
       if not os.path.exists(fileCompPath):   #arrOtnmListAll(i)�Ƿ����
           sameFlag=False #�ļ�������
           break

  if False == sameFlag: #�ļ�ȱʧ�������˳���
       print("3-�ؼ��ļ�ȱʧ�������˳���--" +fileCompPath) 
       #tkMessageBox.showinfo(title=u'���߼����ʾ!', message=u"�ؼ��ļ�ȱʧ�������˳���--" +fileCompPath)
       sys.exit(1)  #�˳�
  print("3-�����Լ��ͨ�������Դ��!")
  
  #a=input("�������������˳���")
  print("5�������˳���")
  time.sleep(5)
  sys.exit(1)  #�˳�
 except Exception, e:
    exstr = traceback.format_exc()
    print( "main(), error!" + '\n' + exstr)
    pass  #����� do nothing 