INSERT INTO [dbo].[store] VALUES
	('Store A','123 street','Edmond','OK','73013');

INSERT INTO [dbo].[person] VALUES 
	(1,1,'Calgary','Michael','73013','1234567890','seth.michael@eagles.oc.edu','ECD71870D1963316A97E3AC3408C9835AD8CF0F3C1BC703527C30265534F75AE','01/01/1900','Admin'),
	(2,1,'Rob','Thompson','73013','2364819238','r.thompson@eagles.oc.edu','ECD71870D1963316A97E3AC3408C9835AD8CF0F3C1BC703527C30265534F75AE','09/08/1986','Customer'),
	(3,1,'Lane','Wheeler','73013','6283948263','lane.wheeler@eagles.oc.edu','ECD71870D1963316A97E3AC3408C9835AD8CF0F3C1BC703527C30265534F75AE','03/05/1994','Pharmacist'),
	(4,1,'Weston','Vidaurri','73013','4059191824','weston.vidaurri@eagles.oc.edu','ECD71870D1963316A97E3AC3408C9835AD8CF0F3C1BC703527C30265534F75AE','04/05/1996','Admin'),
	(5,1,'Weston','Buck','73013','7839231673','weston.buck@eagles.oc.edu','ECD71870D1963316A97E3AC3408C9835AD8CF0F3C1BC703527C30265534F75AE','02/23/1995','Customer'),
	(6,1,'Colby','Dial','73013','3628429834','colby.dial@eagles.oc.edu','ECD71870D1963316A97E3AC3408C9835AD8CF0F3C1BC703527C30265534F75AE','06/13/1997','Pharmacist'),
	(7,1,'Test','Customer','73013','5554445555','customer@test.com','ECD71870D1963316A97E3AC3408C9835AD8CF0F3C1BC703527C30265534F75AE','06/13/1997','Customer'),
	(8,1,'Test','Pharmacist','73013','5554445555','pharmacist@test.com','ECD71870D1963316A97E3AC3408C9835AD8CF0F3C1BC703527C30265534F75AE','06/13/1997','Pharmacist'),
	(9,1,'Test','Admin','73013','5554445555','admin@test.com','ECD71870D1963316A97E3AC3408C9835AD8CF0F3C1BC703527C30265534F75AE','06/13/1997','Admin');

INSERT INTO [dbo].[contact_preference] VALUES
	(1,'Email'),
	(2,'Text'),
	(3,'Email'),
	(4,'Email'),
	(5,'Text'),
	(6,'Phone'),
	(7,'Text'),
	(8,'Text'),
	(9,'None');

INSERT INTO [dbo].[drug] VALUES
    ('60505006501','Omeprazole Cap Delayed Release 20 MG'),
	('591569550','Minocycline HCl Cap 100 MG'),
	('65027225','Olopatadine HCl Ophth Soln 0.2% (Base Equivalent)'),
	('68382012205','Amlodipine Besylate Tab 5 MG'),
	('2416502','Raloxifene HCl Tab 60 MG'),
	('310075190','Rosuvastatin Calcium Tab 10 MG');

INSERT INTO [dbo].[prescription] VALUES
	(1,1,'60505006501','03/06/2016',30,1),
	(2,2,'591569550','03/08/2017',15,3),
	(3,2,'65027225','03/02/2017',30,5),
	(4,5,'68382012205','03/02/2017',30,5),
	(5,4,'68382012205','03/02/2017', 30,3);


INSERT INTO [dbo].[message_history] VALUES
	(1,'yes',null,null,0),
	(2,'no',null,null,0),
	(3,'yes','20170308','20170309',1),
	(4, 'yes', '20170308', null,0);
