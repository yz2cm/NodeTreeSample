using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeTreeSample
{
    public class Record
    {
        public string Id;
        public string ParentId;
    }
    class NodeTree
    {
        public NodeTree(List<Record> inRecordRepository)
        {
            var rootRecords = inRecordRepository.Where(record => record.ParentId == null);

            foreach(var record in rootRecords)
            {
                this.Nodes.Add(new Node()
                {
                    Id = record.Id,
                    ChildNodes = this.buildChildNodesOfId(record.Id, inRecordRepository)
                }) ;
            }
        }

        public List<Node> Nodes { get; } = new List<Node>();

        private List<Node> buildChildNodesOfId(string inNodeId, List<Record> inRecordRepository)
        {
            var childRecords = inRecordRepository.Where(x => x.ParentId == inNodeId);

            if (childRecords.Count() == 0)
            {
                return new List<Node>();
            }

            var childNodes = new List<Node>();
            foreach (var record in childRecords)
            {
                childNodes.Add(new Node()
                {
                    Id = record.Id,
                    ChildNodes = this.buildChildNodesOfId(record.Id, inRecordRepository)
                });
            }

            return childNodes;
        }
    }
    class Node
    {
        public string Id;
        public List<Node> ChildNodes = new List<Node>();
        public void PrintRecursive()
        {
            Console.WriteLine($"Id :{this.Id}");
            foreach(var childNode in this.ChildNodes)
            {
                childNode.PrintRecursive();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var records = new List<Record>()
            {
                new Record() { Id = "100", ParentId = null },
                new Record() { Id = "200", ParentId = null },
                new Record() { Id = "300", ParentId = null },
                new Record() { Id = "400", ParentId = "100" },
                new Record() { Id = "500", ParentId = "400" },
                new Record() { Id = "600", ParentId = "500" },
                new Record() { Id = "700", ParentId = "200" },
            };

            var tree = new NodeTree(records);

            foreach(var rootNode in tree.Nodes)
            {
                rootNode.PrintRecursive();
            }

        }
    }
}
