using System.Collections.Generic;

namespace TableTask.Helper
{
    public class TreeNode<T>
    {
        public TreeNode()
        {
            Children = new List<TreeNode<T>>();
        }
        public T Entity { get; set; }
        public int Level { get; set; }

        public T Key { get; set; }

        public string Title { get; set; }

        public bool IsFolder { get; set; }

        public bool Select { get; set; }
        public List<TreeNode<T>> Children { get; set; }
    }
}
