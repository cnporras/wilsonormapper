# Services #

The main features of WilsonORWrapper are implemented as services. Each service is self-initializing -- all you need to do is set the proper [wiki:Configuration].

There are four services available.

  * **Cache**: Provides access to a data cache.
  * **Data**: Allows you to access your persistence layer (database or otherwise) to read and save entity data.
  * **Logger**: Provides a logging interface.
  * **Query**: Simplifies query generation for your data access commands.


## Data Service ##

The Data service works with the O/R mapper and allows you to read and save data to your persistence layer. It provides static methods to retrieve, save, and delete entities.

The Data service is implemented as a static generic class `Data<T>`, so using the Data service requires you to declare the data type, as shown in the example below.

```
User user = Data<User>.RetrieveFirst();
```

The following methods are exposed by the `Data<T>` class. For detailed information, read the [url:http://www.sidesofmarch.com/wp%2Dcontent/uploads/wilsonorwrapper/docs/ API documentation].

  * **Data**

&lt;T&gt;

.Retrieve()**::
> > Retrieves a collection of objects of type T. Various overloads allow filtering and sorting.
  ***Data

&lt;T&gt;

.RetrieveFirst()**::
> > Retrieves the first object of type T. Various overloads allow filtering and sorting (where sorting returns the "top 1" object).
  ***Data

&lt;T&gt;

.RetrievePage()**::
> > Retrieves a collection of objects of type T based on a given page size and number. Various overloads allow filtering and sorting.
  ***Data

&lt;T&gt;

.Save()**::
> > Saves a given object to the persistence layer. Various overloads support transactions and cascaded saves.
  ***Data

&lt;T&gt;

.Delete()**::
> > Deletes a given object or objects from the persistence layer. Various overloads support transactions and deletions of single or multiple objects.
  ***Data

&lt;T&gt;

.Create()**::
> > Creates a new instance of an object and starts object tracking (necessary for maintaining object relationships).
  ***Data

&lt;T&gt;

.Track()**::
> > Starts tracking for an object that already exists.
  ***Data

&lt;T&gt;

.Insert()**::
> > Inserts an object into the persistence layer as a new object. This is provided for databinding support.
  ***Data

&lt;T&gt;

.Resync()**::
> > Refreshes an object by reloading its data from the persistence layer.**

The Data service also offers two non-generic methods.

  * **Data.ExecuteDataSet(string sqlStatement)''::
> > Executes the sqlStatement and returns the resulting DataSet.
  ***Data.ExecuteScalar(string sqlStatement)''::
> > Executes the sqlStatement and returns an object value.

## Query Service ##

The Query service provides a simple interface to generating OPath queries. It helps simplify code by avoiding the need to explicitly use the Wilson.ORMapper namespace to generate OPathQuery objects.

Using the Query service is best illustrated by comparing how you would generate an OPathQuery using the Wilson.ORMapper namespace, and how you would generate a query using the Query object (which hides the underlying OPathQuery object from your code).

### Querying using OPathQuery

&lt;T&gt;

 ###

```
using Wilson.ORMapper;
using WilsonORWrapper.Services;
...
OPathQuery<User> query = new OPathQuery<User>("Name = ?");
User user = Data<User>.Retrieve(query, "Brian");
```

### Querying using Query

&lt;T&gt;

 ###

```
using WilsonORWrapper.Services;
...
Query<User> query = new Query<User>("Name = ?");
User user = Data<User>.Retrieve(query, "Brian");
```

Note the similarities of the code. The usage of Query

&lt;T&gt;

 is the same as OPathQuery

&lt;T&gt;

, but you are saved the hassle of dealing with a separate namespace.


