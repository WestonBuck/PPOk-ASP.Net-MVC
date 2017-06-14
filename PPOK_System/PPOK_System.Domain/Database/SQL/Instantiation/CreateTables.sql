CREATE TABLE [dbo].[store] (
	[store_id]			int				IDENTITY								NOT NULL,
	[name]				varchar(50)												NOT NULL,
	[address]			varchar(100)													,
	[city]				varchar(30)														,
	[state]				varchar(20)														,
	[zip]				varchar(5)														,
	PRIMARY KEY([store_id])
);


CREATE TABLE [dbo].[person] (
	[person_id]			int														NOT NULL,
	[store_id]			int				REFERENCES [dbo].[store]([store_id])			,
	[first_name]		varchar(15)														,
	[last_name]			varchar(20)														,
	[zip]				varchar(5)														,
	[phone]				varchar(10)														,
	[email]				varchar(30)														,
	[password]			varchar(100)													,
	[date_of_birth]		date															,
	[person_type]		varchar(10)												NOT NUll,
	PRIMARY KEY([person_id])
);


CREATE TABLE [dbo].[contact_preference] (
	[preference_id]		int				IDENTITY								NOT NULL,
	[person_id]			int				REFERENCES [dbo].[person]([person_id])	NOT NULL,
	[contact_type]		varchar(20)												NOT NULL,
	PRIMARY KEY([preference_id])
);


CREATE TABLE [dbo].[drug] (
	[drug_id]			varchar(15)												NOT NULL,
	[drug_name]			varchar(100)											NOT NULL,
	PRIMARY KEY([drug_id])
);


CREATE TABLE [dbo].[prescription] (
	[prescription_id]	int														NOT NULL,
	[person_id]			int				REFERENCES [dbo].[person]([person_id])			,
	[drug_id]			varchar(15)		REFERENCES [dbo].[drug]([drug_id])				,
	[date_filled]		datetime														,
	[days_supply]		int																,
	[number_refills]	int																,
	PRIMARY KEY([prescription_id])
);


CREATE TABLE [dbo].[message_history] (
	[message_id]		int				IDENTITY										NOT NULL,
	[prescription_id]	int				REFERENCES [dbo].[prescription]([prescription_id])		,
	[response]			varchar(100)															,
	[fill_time]			datetime																,
	[pick_up_time]		datetime																,
	[filled]			bit																		,
	PRIMARY KEY([message_id])
);


CREATE TABLE [dbo].[scheduler] (
	[task_id]			int				IDENTITY										NOT NULL,
	[prescription_id]	int				REFERENCES [dbo].[prescription]([prescription_id])		,
	[response]			varchar(100)															,
	[day_to_send]		datetime																,
	PRIMARY KEY([task_id])
);
