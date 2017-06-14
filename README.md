# PPOk
Website for PPOk

## Team
### Old Man and the C:\
* Calgary Michael - https://github.com/CalgaryMichael
* Lane Wheeler - https://github.com/lanecwheeler
* Weston Vidaurri - https://github.com/Wsloan0001
* Rob Thompson
* Weston Buck - https://github.com/WestonBuck
* Colby Dial - https://github.com/CrudenDarkeyes

## Running Web Application
### Initializing
The database will be initialized by launching the website.
If there are any errors building the database on execution, starting the run on "Database/Index" webpage
will manually initialize the database

### Logging in
There are three separate _test_ accounts that can be used to preview the separate pages
**Credentials**:
* _Customer_:
  * _Email_: customer@test.com
  * _Pass_: test123
* _Pharmacist_:
  * _Email_: pharmacist@test.com
  * _Pass_: test123
* _Admin_:
  * _Email_: admin@test.com
  * _Pass_: test123
  
## Layout of Project
### Content
* The _Content_ folder is to hold all client-side code (js, css, Etc.)
* _Content > Vendor_ folder is to hold all third party code (jQuery, Boostrap, Datatables, Etc.)
* _Content > Modules_ folder is to hold all proprietary javascript code

### Data
* The _Data_ folder is to hold all the .csv files used for testing
* _scrubbed_data.csv_ is the file given to the class for test data
* _srubbed_data_alt.csv_ is a file with some updated information

### Service
The _Service_ folder is to hold all server-side code and logic (database, importing logic, Etc.)
* _Authentication_
  * _Password.cs_ - handles logic for determining whether the two passwords match
  * _SHA1.cs_ - handles logic for encrypting a given string (used for password encryption)
* _Import_
  * Handles reading in a given .csv file
  * Handles determining whether a file has any new data in it
  * Handles uploading the updated information to the Database
* _SQL_
  * Each of these scripts will be run upon database initialization
  * They handle the dropping/creation of the Database, the creation of the tables, and the dummy data to be inserted.
* Twilio
  * _TwilioXML_ - the xml needed for Twilio's phone calling service
  * _TwilioManager.cs_ - this handles all interaction with Twilio's API
* _Databas.cs_
  * This handles all interaction with the Database using Dapper.
  * The Creation, CRUD, and Misc. sections are all labeled appropriately 
