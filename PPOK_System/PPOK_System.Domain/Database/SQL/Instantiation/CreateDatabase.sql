USE [master]

IF EXISTS(select * from sys.databases where name = 'PPOk')
    DROP DATABASE PPOk

CREATE DATABASE [PPOk]