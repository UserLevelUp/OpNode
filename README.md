# OpNode - Tree Based Couch DB with a Simple Operations Interface

<!-- <img src="https://repository-images.githubusercontent.com/274567598/c444e080-aac2-48db-9ff6-8d42abc9374c" alt="OpNode Image" /> -->

<img src="https://github.com/UserLevelUp/OpNode/blob/master/Logo%20Sugestions/OpNodeLogo.png" alt="OpNode Image" />

## Dev Info

If you are a developer please get the latest version in the root or master branch - its the default branch.  To open the solution look for the solution or the .sln file is in PWord4 project.   Start from there and it will load all projects and libraries together.  The startup project should be set to MyPword even though the solution is in PWord4 currently.   Also I'm doing development in Visual Studio 2022 so you may not have the best experience in older version of Visual Studio.  

## Welcome

Welcome to OpNode which is a TreeView based system for collecting key value pairs.  Since it is a tree view it can do things like import and export xml files.  It can search through the node structure for a keyword and expand the treenode for quick usage.   Also the OpNode can execute commands and values on a particular node and the results can be copied and dragged to somewhere off the application.   There are many other things OpNode can do now, and will do in the future.   

Goal of the project to to give a face lift to an old project started in 2005 called pWord.  New name is called OpNode to designate individual nodes that contain an operation on a per node basis.  Each Node can have different operations like Add, Subtract, Command Node, any node you can imagine an operational node will encompass it.

OpNode when it was called pWord was originally used as a password vault. So it has uses to store things under a system of grouping under a particular parent.  There are no limit to the depth of nodes you can go, but it's probably better to start a new file and keep the depth shallow as things can get lost if you go to deep in the treeview tree structure.  But if you remember the name you can expand the tree to that point in the treeview.

Create a branch based on the master branch and attempt a pull request.   Please don't post spammy PRs.  

I'll accept anything that is simple, eloquent, and works.

Feel free to add issues or request issues be added.   I'll create a milestone for each type of issue so they are grouped by Milestones.

## So What is OpNode and Whats going on Now

OpNode is about a treenode that contains different operations for a parent, and the operations occur on the child nodes.

Nodes without operations are just key value pairs.

Getting this project ready for Hacktoberfest

## Issues being added for Hactoberfest are:

Create a video of how to use OpNode to attact a larger talent pool to work on this open sourced project.

The old project is on sourceforge named pWord.  I'm renaming it to OpNode.

A more lofty goal of the project is to perform fast operations where it only checks for certain nodes parents to see if its dirty and grabs the results of the parents in the correct order.  This logic has already been added but it needs to be optimized.

The Events within this forms app also needs to be renamed in many cases because the default name was generated and a good name that relates to its function is missing.  So its hard to understand over half of the events used with this Forms Application.

Many of the changes are very difficult and complex, however there will be some tasks that are renaming that are needed.

Testing also needs to be added so functional tests of this forms app can be taken.

Since this treeview and treenode project is a sort of database a datasource and data configurations need to be added to make it possible to add it to future applications as a Couch Tree Spanning database.

Another goal is to make operations be extensible.  Since an IOperate interface just contains one method Operate it is a very simple interface that makes extensions equally easy to add.  However, I haven't had time to do this myself.

Optimizing the file saves as a binary file is another issue that needs to be modernized and made more efficient.  Current it uses a technique a Serialization.  Although its fairly fast, its not very efficient.  It can be made much more efficient on the hard drive.  By making the save less often and saving a current work operations in a separte file that can be added to the larger file later.  This will allow for multithreaded saves which can have a lot of things going on at once more of a possibility.  Right now everything is single threaded and not very efficient.

Another major issue that needs to be resolved is converting operational nodes to XML and from XML back into a treenode structure.  There is a concept of attributes, prefixes, and sufixes and even namespaces and operations for each node.  So this is possible but will require an extensive amount of work.  Though its not hard, it will require a lot of testing.

Converting the treeview model to JSON and from JSON back to a tree node structure. Very difficult problem.  The XML is not very optimized but works.  So same kind of logic but for JSON objects.  Note: You can select a specific node anywhere on the tree today and generate XML.  This needs to happen in reverse where you can select anywhere on the tree and export or import JSON not just XML.

Adding a JSON Schema operation to a parent node so all children follow it is also an issue on the extremely hard list.

Also need to add XML schema.

Add an XSLT operations to a particular node.

Enhance the history so it can record commands in the history with undo and redo features.  

Make OpNode multi-threadeda application.  Very difficult problem to solve.

Get the command prompt working as well so that it can add nodes and even operational nodes using the command line.  So when you run the application which is currently named pWord it opens up an application and at the bottom there should be four tabs.  The last tab is the command line.  This is a very basic command line and history recorder right now on master.  Its a work in progress and its rather new but has low value right now as its not really useful part of a workflow  That will change once undo and redo functionality becomes standard in the OpNode application.

Get the command prompt working more seemlessly so it has better history for command, powershell, bash, terminal prompt, nodejs prompt, etc...

May need a project manager to help with issues.  Adding issues, upgrading or down grading issues.  Prioritizing sets of tasks so they fall in order when resources are available to tackle them.

The installer initially was based on a free version of InstallShield so I switched to Wix which is free and open source.  But may need a massive amount of help as Wix is complex and murky installer for executables and resource files.

### If You Are New To Git

You can download a zip version of the project.  But its better to first install Git from the GitSCM approved website and then once that installed then open up a new command prompt or use a git client app like tortoisegit.  You'll need to create a folder dedicated to the project such as c:\src\opnode.  Change directory to that folder.  Then you'll need to run the git clone command to install the repostiory form github into your opnode folder.

<pre>
mkdir opnode
cd opnode
git clone https://github.com/UserLevelUp/OpNode.git .
</pre>

### Other Notes

If you add an issue, please request it to be looked over by myself and I will add any tags or remarks to the issue.  Otherwise I'm likely to delete the issue especially if  it doesn't relate with the goals of the project.  But I'm more than happy to work with you to make it fit into the goals of this project.
