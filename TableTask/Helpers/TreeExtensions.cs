using System;
using System.Collections.Generic;
using System.Linq;

namespace TableTask.Helper
{
    public static class TreeExtensions
    {
        private static IEnumerable<TreeNode<TEntity>> CreateHierarchy<TEntity, TProperty>(IEnumerable<TEntity> items, TEntity parentItem,
            Func<TEntity, TProperty> idProperty, Func<TEntity, TProperty> parentIdProperty, int level, Func<TEntity, TProperty> orderBy) where TEntity : class
        {
            if (orderBy != null)
            {
                items = items.OrderBy(orderBy);
            }
            IEnumerable<TEntity> childs;
            var entities = items as TEntity[] ?? items.ToArray();
            if (parentItem == null)
            {
                childs = entities.Where(i => parentIdProperty(i).Equals(default(TProperty)));
            }
            else
            {
                childs = entities.Where(i => parentIdProperty(i).Equals(idProperty(parentItem)));
            }

            var enumerable = childs as TEntity[] ?? childs.ToArray();
            if (enumerable.Any())
            {
                level++;
                foreach (var item in enumerable)
                {
                    yield return new TreeNode<TEntity>
                    {
                        Entity = item,
                        Children = CreateHierarchy(entities, item, idProperty, parentIdProperty, level, orderBy).ToList(),
                        Level = level
                    };
                }
            }
        }

        public static IEnumerable<TreeNode<TEntity>> AsHierarchy<TEntity, TProperty>(this IEnumerable<TEntity> items, Func<TEntity, TProperty> idProperty, Func<TEntity, TProperty> parentIdProperty, Func<TEntity, TProperty> orderBy) where TEntity : class
        {
            return CreateHierarchy(items, default(TEntity), idProperty, parentIdProperty, 0, orderBy);
        }

        public static IEnumerable<TreeNode<TKey>> AsTreeNodes<TKey, TEntity>(this IEnumerable<TreeNode<TEntity>> nodes, Func<TEntity, int, TreeNode<TKey>> mapping)
        {
            var rootNode = new List<TreeNode<TKey>>();

            Action<TreeNode<TKey>, List<TreeNode<TEntity>>, Func<TEntity, int, TreeNode<TKey>>> createTree = null;
            createTree = (parent, children, map) =>
            {
                foreach (var child in children)
                {
                    var cParent = mapping(child.Entity, child.Children.Count);
                    parent.Children.Add(cParent);

                    if (child.Children.Count > 0)
                    {
                        createTree(cParent, child.Children, map);
                    }
                }
            };

            foreach (var node in nodes)
            {
                var parent = mapping(node.Entity, node.Children.Count);
                rootNode.Add(parent);
                createTree(parent, node.Children, mapping);
            }
            return rootNode;
        }


        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> items, Func<T, IEnumerable<T>> children)
        {
            return items.SelectMany(c => children(c).Flatten(children)).Concat(items);
        }

        public static void Traverse<T>(this IEnumerable<T> items, T parent, Func<T, IEnumerable<T>> children, Action<T, T, int> callback, int index = 0)
        {
            foreach (var item in items)
            {
                callback(parent, item, index++);

                var child = children(item);
                if (child.Any())
                {
                    Traverse(child, item, children, callback, 0);
                }
            }
        }
    }
}