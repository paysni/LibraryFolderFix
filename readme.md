<!---
	Atlas of Information Management business intelligence library and documentation database.
  Copyright (C) 2020  Riverside Healthcare, Kankakee, IL

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with this program.  If not, see <https://www.gnu.org/licenses/>.
-->

# Atlas Documentation

## Live Demo
> :point_right: **[Live Atlas Demo with Docker](https://hub.docker.com/r/christopherpickering/rmc-atlas-demo)**

Demo is running with Docker and Ubuntu 16.04. Check out the Dockerfile for build steps.

App can be run locally with our public docker image -
```sh
docker run -i -t -p 1234:1234 -e PORT=1234  -u 0 christopherpickering/rmc-atlas-demo:latest
```
or, you can clone the repo and build your own docker image -
```sh
docker build  --tag atlas_demo .
docker run -i -t -p 1234:1234 -e PORT=1234  -u 0 atlas_demo:latest
# web app will be running @ http://localhost:1234
# see Dockerfile for db access
```

### Atlas can be run in an [online sandbox](https://labs.play-with-docker.com/) - it does require a docker.com login.

1. Click "start"
2. Click "Settings" > 1 Manager and 1 Worker
3. Click on the Manager instance. Atlas is large and doesn't run in the worker.
4. Paste in ```docker run -i -t -p 1234:1234 -e PORT=1234  -u 0 christopherpickering/rmc-atlas-demo:latest```
5. Wait about 1-2 mins for app to download and startup. Output will say ```Now listening on: http://[::]:1234``` when ready.
6. Click "Open Port" and type ```1234```
7. App will open in new tab. The URL should be valid for 3-4 hrs.

## Credits

Atlas was created by the Riverside Healthcare Analytics team -

* Paula Courville
* [Darrel Drake](https://www.linkedin.com/in/darrel-drake-57562529)
* [Dee Anna Hillebrand](https://github.com/DHillebrand2016)
* [Scott Manley](https://github.com/Scott-Manley)
* [Madeline Matz](mailto:mmatz@RHC.net)
* [Christopher Pickering](https://github.com/christopherpickering).
* [Dan Ryan](https://github.com/danryan1011).
* [Richard Schissler](https://github.com/schiss152).
* [Eric Shultz](https://github.com/eshultz).

## Getting Started

### Requirements
1. SQL Server Database (we use 2016 or newer, any license type) with Full Text Index installed
2. IIS Webserver with Microsoft .NET Core SDK 2.2.105 (x86)
3. Dev Machine w/ the following
    * Visual Studio + Analysis Services, extensions for SSIS Integration Services
    * Python 3.7, virtualenv
    * Active Directory Explorer, or other access to Active Directory
    * Microsoft .NET Core SDK 2.2.105 (x86)

Atlas can run on any server OS that is capable of running Dotnet 2.2, and the database on any server that is capable of running Sql Server + Full Text Index.

### Steps to Run
1. Run database create scripts (LDAP, Data Governance, DG Staging). Set Datagov user credentials in database.
2. Run Data Governance database seeding script
2. Configure and run python LDAP script. Modify script and settings.py to match your LDAP build. Schedule in SQL Agent Jobs
3. Configure and run Atlas ETL's (main ETL and run data)
    * Delete SSRS sections if not used
    * Update Clarity server and credentials
    * Update Database connection strings
    * Schedule ETL's in SQL Agent Jobs
4. Populate Atlas/appsettings.json & appsettings.Development.json
5. Run website locally in Visual Studio
6. Update publish settings & publish

## Web App

### Structure

The app is built using C# Razor Pages, HTML, CSS, Javascript and Jquery. The app is very ajax heavy, in an effort to make the content appear when needed, quickly.

The app is setup following these general guidelines

* Each url path recieves it own page in app/pages folder
* Javascript & CSS files are generally named after the pages they represent
* Ajax views are generally kept in the cs file the pull data from

### Database

The app is currently using SQL Server, with a Database First model.

The database can be created by running the script "weba;;/DatabaseCreationScript.sql".

#### Updates to Database

Updates to the database can be moved into the webapp by running the following scafold commmand:

```

# after running this commande we need to manually delete connection string from db context file

Scaffold-DbContext "Server=rhbidb01;Database=Data_Governance_Dev;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force

```

Upon DB changes the file "webapp/DatabaseCreationScript.sql" should be updated. This can be done in SSMS by right clicking the db > clicking tasks > script database.


#### Customizing Model with Partial Classes
Customizations for a model are created in the model/customizations/ folder.

## ETL

There are two ETL's in the project. The first (ETL) brings in the report & user data. The second ETL (ETL-Run Data) brings in the run data.

### User Data
Most user data is imported from LDAP You can scheduled the scripts from /LDAP to run daily on your SQL Server.

Our org removes domain accounts after a user leaves the org, so we also import any missing users from the SSRS Report Db's and Epics User list.


## Hyperspace

Linking to Atlas from a Dashboard
1. Create a link component
2. Select a source of Component Editor
3. Select an Activity type link
4. Give your link a label
5. In the parameters, list the Atlas activity, and then runparams:<URL of the content> (like below)
```
ATLAS_WEBSITE,RunParams:https://atlas.riversidehealthcare.net/Reports?id=73740
```
6. For the above, I picked a random report entry in Atlas, but the principle should stand for any specific atlas URL


## Webapp UI

Flex > column can only be used on the main container w/ the IE fix. IE has a bug ignoring height and if this is used in other places there will be pain :) flex > row can be used.

### CSS
follows www.maintainablecss.com.

### Tabs
### Carousel
### Modal
### Progress bar
### Tooltips
### Drag and Drop


### Ajax content

content can be dynamically loaded by creating a div with a few attributes. The returned content *must* be wrapped in a single tag.
```html
<div data-ajax="yes" data-url="pate/to/page" data-param="stuff to append to url" data-loadtag="optional tag to load from response"></div>
```
Add the "cache" attribute to attempt to locally cache the data. The data will cache for 30 mins.

The dynamic content must be wrapped with a single element as it will completely replace the original element. The data attributes will be copied to the new content so that it will be possible to refresh it later on.

### Ajax page

add the class "ajax" to any `<a href ></a>` to use ajax to load the page. add the "cache" attribute to attempt to cache the page locally.