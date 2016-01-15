WilsonORMapper is the open source release of Paul Wilson's O/R Mapper. It is lightweight, powerful, and easy-to-use.

Originally hosted at http://www.ormapper.net, the open source release was made official on Sept 9 2007 by Paul Wilson ([read his announcement](http://ormapper.net/Forums/Default.aspx?part=74&action=thread&id=2580&key=wyjyckxZYAKD9WiUMF8c4Q%3d%3d)), as of version 4.2.2.0.

## .NET v2.0 Support ##
  * Supports both Generics and Nullable Types with the .NET v2.0 Build.

## Database Support ##
  * Supports MS Sql, Access, Oracle, as well as generic OleDb and Odbc.
  * Define your own CustomProvider for any Database without Recompile:
  * MySql, PostgreSql, Sqlite, Firebird, DB2, VistaDB, Sybase, etc.
  * Retrieve individual objects by key -- auto, guids, and user-entered.
  * Use auto key-types with Oracle 9+ by setting trigger with sequence.
  * Composite keys supported, even with relations and stored procs.
  * Supports one-to-many, many-to-one, and many-to-many relationships.
  * Supports outer joins to populate lookup values in a single query.
  * Supports optional lazy-loading to minimize reads from the database.
  * Supports optional default sort orders that are always overridable.
  * Supports optional field default null-values -- thanks to Tim Byng.
  * Even use null-value expressions like MinValue -- thanks to Jeff Lanning.
  * Use any raw sql statement with GetDataSet or ExecuteCommand.

## Retrieval Support ##
  * Retrieve individual objects by primary key, often from internal cache.
  * Retrieve collections as a static ObjectSet or a cursor ObjectReader.
  * GetCollection returns strongly typed collections -- thanks to Allan Ritchie.
  * Retrieve DataSets, optionally specifying a specific subset of fields.
  * Query with any where and sort clauses -- use sql or the QueryHelper.
  * Build simple expressions with an OPath-like syntax using the QueryHelper.
  * Includes nearly complete OPath query engine -- thanks to Jeff Lanning.
  * Supports paged collections -- queries can specify page index and size.
  * Can optionally use stored procedures with parameter for any retrieval.
  * Performance is very much comparable, sometimes better, than DataSets.
  * Support for Typed Datasets which some Reporting Packages Require.

## Persistence Support ##
  * Explicitly create objects with new and StartTracking, or use GetObject.
  * Persist individual objects, or a collection of objects in transaction.
  * Control your own Transaction object with BeginTransaction method.
  * Interact with the Transaction in the IObjectNotification events.
  * Thanks to Ken Muse for recursive persistence and cascading delete help.
  * Recursively persist all related child changes with PersistDepth.ObjectGraph.
  * Optionally enable cascading deletes on any relation in the xml mappings.
  * User can define fields for optimistic concurrency, or read-only fields.
  * Updates can optionally be only the changes, with or without concurrency.
  * Execute set based updates and deletes, including update set expressions.
  * Persistence is done with safe and efficient parameterized dynamic sql,
  * although stored procedures can optionally be configured for persistence.
  * Supports optional before and after event notification for persistence.

## Simple Architecture ##
  * No need to inherit objects from a base class or embed any attributes.
  * Map either member fields or properties, with reflection or interface.
  * Object-relational mappings are made in one or more simple xml files.
  * Optionally use embedded resources for mappings -- thanks to Allan Ritchie.
  * Support class hierarchies and simple mapping inheritance by Jerry Shea.
  * Also supports "shared inheritance" in the database using type discriminator.
  * Optionally intercept database commands for logging or even modifications.
  * Includes a Windows ORHelper to generate the mapping and class files.
  * Also includes CodeSmith Templates from Paul Welter for code generation.
  * Collections support one-way read-only binding for both Web and Windows.
  * Separate instances can be created for multiple databases or providers.
  * Easy to deploy with simple x-copy in shared web hosting environments.
  * Supports medium-trust when mapping to public members (or reflection).
  * Includes full C# source code, as well as the rest of WilsonDotNet.com.