using System;
using System.Windows.Forms;
using pWordLib.dat;
namespace pWordLib
{

public struct Image : IEquatable<Image>
{
private const int groupup = 0;
private const int groupdown = 1;
private const int scriptup = 2;
private const int scriptdown = 3;
private const int usernameup = 4;
private const int usernamedown = 5;
private const int passwordup = 6;
private const int passworddown = 7;
private const int websiteup = 8;
private const int websitedown = 9;
private const int ftpsiteup = 10;
private const int ftpsitedown = 10;

			
public int GroupUp
{
get
{
return groupup;
}
}
public int GroupDown
{
get
{
return groupdown;
}
}
public int ScriptUp
{
get
{
return scriptup;
}
}
public int ScriptDown
{
get
{
return scriptdown;
}
}
public int UsernameUp
{
get
{
return usernameup;
}
}
public int UsernameDown
{
get
{
return usernamedown;
}
}
		// Not really a user for this yet.  Not sure what I originally intended for images per node
		// but updated IEquatable interface
        public bool Equals(Image other)
        {
            return other.UsernameDown == this.UsernameDown && other.UsernameUp == this.UsernameUp && other.ScriptDown == this.ScriptDown && other.ScriptUp == this.ScriptUp && other.GroupDown == this.GroupDown && other.GroupUp == this.GroupUp;
        }
    }
	public struct TreePics : IEquatable<TreePics>
	{
		private readonly pNode picnode;

		public TreePics(string name,int img1,int img2)
		{
			picnode = new pNode(name,img1,img2);
		}

		public pNode PicNode
		{
			get
			{
				return picnode;
			}
		}

        public bool Equals(TreePics other)
        {
            return other.PicNode.Name == this.picnode.Name && other.picnode.ImageIndex == this.picnode.ImageIndex && other.picnode.SelectedImageIndex == this.picnode.SelectedImageIndex;
        }
    }

}

