# OpNode Tree Based Database

Create a branch based on the master branch and attempt a pull request.   Please don't post spammy PRs.  

I'll accept anything that is simple, eloquent, and works.

Issues being added for Hactoberfest are:

Feel free to add issues or request issues be added.   I'll create a milestone for each type of issue so they are grouped by Milestones.

Create a video of how to use OpNode to attact a larger talent pool to work on this open sourced project.

OpNode is about a treenode that contains different operations for a parent, and the operations occur on the child nodes.

Nodes without operations are just key value pairs.

Getting this project ready for Hacktoberfest

Goal of the project to to give a face lift to an old project started in 2005 called pWord.  New name is called OpNode to designate individual nodes that contain an operation on a per node basis.  Each Node can have different operations like Add, Subtract, Command Node, any node you can imagine an operational node will encompass it.

The old project is on sourceforge named pWord.  I'm renaming it to OpNode.

A more lofty goal of the project is to perform fast operations where it only checks for certain nodes parents to see if its dirty and grabs the results of the parents in the correct order.  This logic has already been added but it needs to be optimized.

The Events within this forms app also needs to be renamed in many cases because the default name was generated and a good name that relates to its function is missing.  So its hard to understand over half of the events used with this Forms Application.

Many of the changes are very difficult and complex, however there will be some tasks that are renaming that are needed.

Testing also needs to be added so functional tests of this forms app can be taken.

Since this treeview and treenode project is a sort of database a datasource and data configurations need to be added to make it possible to add it to future applications as a Couch Tree Spanning database.

Another goal is to make operations be extensible.  Since an IOperate interface just contains one method Operate it is a very simple interface that makes extensions equally easy to add.  However, I haven't had time to do this myself.

Optimizing the file saves as a binary file is another issue that needs to be modernized and made more efficient.  Current it uses a technique a Serialization.  Although its fairly fast, its not very efficient.  It can be made much more efficient on the hard drive.  By making the save less often and saving a current work operations in a separte file that can be added to the larger file later.  This will allow for multithreaded saves which can have a lot of things going on at once more of a possibility.  Right now everything is single threaded and not very efficient.

Another major issue that needs to be resolved is converting operational nodes to XML and from XML back into a treenode structure.  There is a concept of attributes, prefixes, and sufixes and even namespaces and operations for each node.  So this is possible but will require an extensive amount of work.  Though its not hard, it will require a lot of testing.

Converting the model to JSON and from JSON back to a tree node structure. Very difficult problem.  The XML is not very optimized but works.  So same kind of logic but for JSON objects.

Adding a JSON Schema operation to a parent node so all children follow it is also an issue on the extremely hard list.

Make OpNode multi-threadeda application.  Very difficult problem to solve.

Get the command prompt working as well so that it can add nodes and even operational nodes using the command line.

Get the command prompt working more seemlessly so it has better history for command, powershell, bash, terminal prompt, nodejs prompt, etc...

May need aproject manager to help with issues.

