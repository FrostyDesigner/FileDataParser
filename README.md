# FileDataParser

This project is actually part one of two complete projects. Both projects accomplish the exact same task.
One project is written in c# and the other project is written in Python. The python version of this project is included in its own repo.

The purpose of the 2 projects side by side is to compare time to code in each as well as performance of each completed project.

Essentially c# vs Python.
____________________________________


There are times when you are delivered a source file that must be parsed in order to put the data into a table format.
We put it into a table so it can be read by a database or other data processing object.
Below is an example of the data that these programs parse.
In both cases we will read data like this, store it as a data table of some kind and write it out to a csv file.

If you would like to generate a larger data set than the example below:
copy and paste the following into a windows command terminal.
dir /s /o > C:\myFiles.txt

It will generate a similar directory listing starting from thet location the cmd terminal is in.


 Directory of C:\Program Files\PostgreSQL

07/10/2018  05:50 PM    <DIR>          .
07/10/2018  05:50 PM    <DIR>          ..
07/10/2018  05:49 PM    <DIR>          conf
07/10/2018  05:49 PM    <DIR>          data
07/10/2018  05:48 PM    <DIR>          hub
07/10/2018  05:50 PM    <DIR>          logs
07/10/2018  05:49 PM    <DIR>          pg10
05/31/2017  05:02 AM    <DIR>          pgadmin3
07/10/2018  05:49 PM    <DIR>          python2
07/10/2018  05:50 PM    <DIR>          uninstall
05/10/2018  05:42 AM             1,862 pgc.bat
               1 File(s)          1,862 bytes

 Directory of C:\Program Files\PostgreSQL\conf

07/10/2018  05:49 PM    <DIR>          .
07/10/2018  05:49 PM    <DIR>          ..
07/10/2018  05:49 PM    <DIR>          backup
07/10/2018  05:49 PM    <DIR>          cache
07/10/2018  05:48 PM    <DIR>          images
07/10/2018  05:49 PM                 2 pgc.pid
07/10/2018  05:49 PM            78,848 pgc_local.db
07/10/2018  05:49 PM            43,755 versions.sql
07/10/2018  05:49 PM               143 versions.sql.sha512
               4 File(s)        122,748 bytes
