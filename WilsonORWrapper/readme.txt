
WILSON O/R WRAPPER
==================


What is it?
-----------

WilsonORWrapper is a library that wraps around Paul Wilson's O/R Mapper 
("WORM") that creates entity objects and a service layer for your O/R mapper 
objects. It is implemented as a set of assemblies that provide base 
functionality and a set of CodeSmith templates that can autogenerate your 
entity and service layers.


Who wrote it?
-------------

The author of the WilsonORWrapper project is Brian DeMarzo. However, a large 
part of the code contained within it is derived from CodeSmith templates by 
Paul Welter. Other smaller code blocks are derived from general tips pulled 
from various web sources; credits to the authors where relevant are given 
within the source code.


Other contributors
------------------

As an open source project, we accept patches from anyone willing to write
one. The following folks have provided patches that have been incorporated
(in full or part) to this project, or who have offered advice or guidance
that has proven valuable to this project.

Gunter Spranz
Wayde Gilliam
John Nuechterlein
Matthew Barrett
Michael Mepham


How to use it
-------------

To use the wrapper, you first need to do three things: create your database, 
create your mappings file, and create your projects. 

CodeSmith templates are provided to build your mappings file, which you 
should always review and edit manually after creation. 

CodeSmith templates are provided to build your entity and service classes. 
These templates will also create or update a Visual Studio project for both 
your entity and service layers, and will optionally create a test project 
(NUnit required).

Further instructions can be found at the WilsonORWrapper web site.


Support
-------

To get support, post a message on the WilsonORWrapper Google group page
at http://groups.google.com/group/wilsonorwrapper.


How to build it
---------------

To build the assemblies, you need the source code release and either 
NAnt (recommended) or Visual Studio 2005.

To build using NAnt, use the following command from the directory
containing the default.build file (included with the source code).

	> nant

By default, code will be compiled with debugging information; to disable 
debugging (i.e. release code), use the following command.

	> nant -D:debug=false

To build using Visual Studio 2005, open the WilsonORWrapper.sln solution
and build it. It is suggested that you create a \bin subfolder in the
folder containing the solution file and copy all created binaries to
that folder. Failure to do this will result in the inability to run
examples out-of-the-box.


Third-Party Libraries
---------------------

In addition to those previously mentioned, this project uses the following 
third-party libraries. Each come with their own licenses which are are 
included alongside their binaries.

	Wilson O/RMapper (core O/R mapper)
	NLog (logging service)
	log4net (logging service)
	memcacheddotnet (client for memcached caching service)



Licensing Info
--------------

This product is provided under the University of Illinois/NCSA Open Source 
License. See the license.txt file included in this distribution.


Links and Contact Info
----------------------

Google Code Project Site:
	http://code.google.com/p/wilsonormapper/
Subversion Repository:
	https://wilsonormapper.googlecode.com/svn/trunk/

Paul Wilson's O/R Mapper (original site):
	http://www.ormapper.net/
CodeSmith:
	http://www.codesmithtools.com/
NAnt:
	http://nant.sourceforge.net/
NLog:
	http://www.nlog-project.org/
log4net:
	http://logging.apache.org/log4net/
memcached:
	http://www.danga.com/memcached/
memcacheddotnet:
	https://sourceforge.net/projects/memcacheddotnet/
	
Brian DeMarzo's web site:
	http://www.sidesofmarch.com/
Paul Wilson's web site:
	http://www.wilsondotnet.com/
Paul Welter's web site:
	http://www.loresoft.com/
