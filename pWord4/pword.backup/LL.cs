using System;
using System.Windows.Forms;


namespace LL
{
	/// <summary>
	/// Summary description for LL.
	/// </summary>
	/// 
	[Serializable]
	class ListNode
	{
		private object data;
		private ListNode next;

		// Constructor to create ListNode that refers to dataValue
		// and is last node in the list
		public ListNode( object dataValue ) : this(dataValue, null)
		{
		}

		// constructor to create List NOde that refers to dataValue
		// and refers to next ListNode in the List
		public ListNode( object dataValue, ListNode nextNode)
		{

			data = dataValue;
			next = nextNode;
		}

		// property Next
		public ListNode Next
		{
			get
			{
				return next;
			}
			set 
			{
				next = value;
			}
		}
		// Property Data
		public object Data
		{
			get
			{
				return data;
			}
		}


	

	} // end class ListNode

	[Serializable]
	public class List
	{
		private ListNode firstNode;
		private ListNode lastNode;
		private string name;  // string like "list" to display

		// construct empty List with specified name
		public List( string listName )
		{
			name = listName;
			firstNode = lastNode = null;
		}

		// construct empty List with "list" as its name
		public List() : this( "list" )
		{
		}

		// Insert object at front of List. If List is empty.
		// firstNode and lastNode will refer to same object.
		// Otherwise, firstNode refers to new node.

		public void InsertAtFront( object insertItem )
		{
			lock (this)
			{
				if (IsEmpty() )
					firstNode = lastNode = new ListNode(insertItem);
				else
					firstNode = new ListNode (insertItem, firstNode);
			}
		}

		// Insert Object at end of List.  If List is empty
		// firstNode and lastNode will refer to same object.
		// Otherwise, lastNode's Next property refers to new node.
	
		public void InsertAtBack( object insertItem)
		{
			lock (this)
			{
				if (IsEmpty())
					firstNode = lastNode = new ListNode(insertItem);
				else 
					lastNode = lastNode.Next = new ListNode(insertItem);
			}
		}

		// remove first node from List
		public object RemoveFromFront()
		{
			lock ( this )
			{
				if (IsEmpty() )
					throw new EmptyListException(name);
				object removeItem = firstNode.Data;  // retrieve data

				// reset firstNode and lastNode references
				if ( firstNode == lastNode)
					firstNode = lastNode = null;
				else
					firstNode = firstNode.Next;
				return removeItem;  // return removed data
			}
		}

		// remove last node from list
		public object RemoveFromBack()
		{
			lock (this)
			{
				if (IsEmpty())
					throw new EmptyListException(name);
				object removeItem = lastNode.Data;  // retrieve data

				// reset firstNode and lastNode references
				if ( firstNode == lastNode )
					firstNode = lastNode = null;
				else
				{
					ListNode current = firstNode;
					// loop while current node is not lastNode
					while (current.Next != lastNode)
						current = current.Next;  // move to next node
					// current is new lastNode
					lastNode = current;
					current.Next = null;

				}
				return removeItem;  // return removed data
			}
		}

		// return true if List is empty
		public bool IsEmpty()
		{
			lock ( this )
			{
				return firstNode == null;
			}
		}

		// output List contents
		virtual public void Print()
		{
			lock ( this )
			{
				if (IsEmpty() )
				{
					Console.WriteLine("Empty " + name );
					return;
				}

				//Console.Write("The " + name + " is: ");
				ListNode current = firstNode;

				// output current node data while not at end of the list
				while ( current != null)
				{

					//	Console.Write( current.Data + " " );
					current = current.Next;
				}
				//Console.WriteLine("\n");
			}
		}
	
		virtual public int countme()
		{
			int count = 0;
			lock ( this )
			{
				if (IsEmpty() )
				{
					//	Console.WriteLine("Empty " + name );
					return count;
				}

				//				Console.Write("The " + name + " is: ");
				ListNode current = firstNode;

				// output current node data while not at end of the list
				while ( current != null)
				{

					count++;
					current = current.Next;
				}
			}

			return count;
		}

		// name is of the name of the linked list requested to insert the value into

//		virtual public bool Add(string name, string val)
//		{
//			//	int count = countme();
//			lock (this)
//			{
//				//	if (IsEmpty() )
//				//	{
//				//		return false;
//				//	}
//				
//				ListNode current = firstNode;
//				while ( current != null)
//				{
//
//					List tst = (List)current.Data;
//					if (tst.name == name)
//					{
//						tst.InsertAtBack(val);
//						return true;
//					}
//					current = current.Next;
//				}
//
//			}
//			return false;
//
//		}

		// name is of the name of the linked list requested to insert the value into
//		virtual public string Start(string name)
//		{
//			//	int count = countme();
//			lock (this)
//			{
//				//	if (IsEmpty() )
//				//	{
//				//		return false;
//				//	}
//				
//				ListNode current = firstNode;
//				while ( current != null)
//				{
//
//					List tst = (List)current.Data;
//					if (tst.name == name)
//					{
//						return (string)tst.firstNode.Data; // assuming it is a string
//					}
//					current = current.Next;
//				}
//
//			}
//			return "";
//
//		}

		// name is of the name of the linked list requested to insert the value into
//		virtual public string Next(string name, int counter)
//		{
//			
//
//	
//			lock (this)
//			{
//				//	if (IsEmpty() )
//				//	{
//				//		return false;
//				//	}
//				
//				ListNode current = firstNode;
//			
//				while ( current != null)
//				{
//
//					List tst = (List)current.Data;
//					if (tst.name == name)
//					{
//						int count;
//						count = tst.countme();
//						if (counter > count)
//							return null;
//						ListNode current2 = tst.firstNode;
//						while ( counter-- > 1 )
//							current2 = current2.Next;
//						return (string)current2.Data; // assuming it is a string
//					}
//					current = current.Next;
//				}
//
//			}
//			return "";
//
//		}

//		virtual public bool PList(ListBox listy, ProgressBar progy)
//		{
//			int count = countme();
//			lock ( this )
//			{
//				if (IsEmpty() )
//				{
//					return false;
//				}
//
//
//				ListNode current = firstNode;
//
//				// output current node data while not at end of the list
//				int i=0,j;
//				while ( current != null)
//				{
//					listy.Items.Add(current.Data.ToString());
//
//					current = current.Next;
//					if ((++i%10) == 0) 
//					{
//
//						j = (int)(((float)(((float)i)/((float)count)))*100);
//						progy.Value = j;
//
//						//progy.Update();
//						//progy.Parent.Refresh();
//						
//No subprojects have					}
//
//					//System.Threading.Thread.Sleep(50);
//
//				}
//				progy.Value = 100; // tell the progress bar we are done
//			}
//			return true;
//
//		}
//	

		

	} // End class List




	// class EmptyListException definition
	public class EmptyListException : ApplicationException
	{
		public EmptyListException(string name) : base("The " + name + " is empty")
		{
		}
	} // end class EmptyListException

} // end namespace Linked ListLibrary

