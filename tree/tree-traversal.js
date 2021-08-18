/*
TreeNode {
   val: {number},
   children: {TreeNode[]}
}
 */

class TreeNode {
   constructor(val, children) {
      this.val = val;
      this.children = children || [];
   }
}

const dfsRecursive = function(node, output) {
   if (!node?.children) {
      return;
   }

   output.push(node.val);
   for (const child of node.children) {
      dfsRecursive(child, output);
   }
};

const bfs = function(root) {
   if (!root) {
      return;
   }

   const output = [];
   const queue = [root];
   while (queue.length > 0) {
      const node = queue.shift();

      for (const child of node.children) {
         queue.push(child);
      }

      output.push(node.val);
   }

   return output;
};



/*
Tree Initialisation
*/
const rootChildren = [];
const secondNodeChildren = [new TreeNode(13, [new TreeNode(139)]), new TreeNode(15)];
rootChildren.push(new TreeNode(2, secondNodeChildren));
rootChildren.push(new TreeNode(7));
rootChildren.push(new TreeNode(8));
const root = new TreeNode(1, rootChildren);
   /*
     Tree Visualisation
               1
             / | \
            2  7  8
           / \
          13 15
         /
        139
   */

// BFS Breadth-First Search
console.log(bfs(root));

// DFS Depth-First Search Recursive (порядок обхода вершин называется PreOrder)
const result = [];
dfsRecursive(root, result);
console.log(result);

// h/w: implement DFS iteratively yourself
// ...