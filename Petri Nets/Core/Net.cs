using Petri_Nets.Models;
using Petri_Nets.Tree;
using Petri_Nets.TreeNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Petri_Nets.Core
{
    public class Net
    {
        /// <summary>
        /// Метод для вычитания меток из позиций
        /// </summary>
        /// <param name="node"></param>
        private void Sub(Node node)
        {
            var place = (Place)node;

            place.Tokens--;
        }

        private void Add(Node node)
        {
            var place = (Place)node;

            place.Tokens++;
        }

        /// <summary>
        /// Проверка перехода на разрешенность
        /// </summary>
        /// <param name="transition"></param>
        /// <returns></returns>
        public bool CheckAllowTrantion(Transition transition)
        {
            //данные для посчета фишек для разрешающего перехода
            var countLink = 0;
            var countTokens = 0;

            var fake = transition.Copy();

            foreach (var linked in fake.Previous)
            {
                countLink++;

                var place = (Place)linked;

                if (place.Tokens > 0)
                {
                    countTokens++;
                    place.Tokens--;
                }
            }

            if (countTokens >= countLink && transition.Next.Count != 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Все разрешенные на данный момент переходы
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Model AllAllowTransitionOnStep(Model model)
        {
            var allowTransitions = new Model();

            foreach (Transition transition in model.Where(n => n is Transition).ToList())
            {
                var allow = CheckAllowTrantion(transition);

                if (allow == true)
                {
                    allowTransitions.Add(transition);
                }
            }

            return allowTransitions;
        }

        public void OneStepExecuteNet(Transition transition)
        {
            transition.Previous.ForEach(Sub);
            transition.Next.ForEach(Add);
        }

        public int[] M(Model model)
        {
            var count = model.Where(x => x is Place).Count();

            var M = new int[count];

            //Для записи в виде [1,0,0,1], т.к. кол-во позиций разное в сетях
            var i = 0;

            foreach (var node in model)
            {
                if (node is Place)
                {
                    var place = (Place)node;

                    M[i] = place.Tokens;

                    i++;
                }
            }

            return M;
        }

        public void AnalysisNet(Model model)
        {
            //Корневой узел
            var root = CreateNodeTree(model);

            //Все узлы, для проверки нет ли повторяющихся
            var allNodes = new List<NodeTree>();

            var tree = new NTree<NodeTree>(root.data);

            TreeBuilding(tree, allNodes);
        }

        public NTree<NodeTree> CreateNodeTree(Model netState)
        {
            //Состояние сети до срабатывания перехода
            var copyNetState = netState.Copy();

            var mNet = M(copyNetState);

            var data = new NodeTree()
            {
                MNet = mNet,
                NetState = copyNetState
            };

            var newNode = new NTree<NodeTree>(data);

            return newNode;
        }

        public void TreeBuilding(NTree<NodeTree> tree, List<NodeTree> nodes)
        {
            var allAllowTransition = AllAllowTransitionOnStep(tree.data.NetState);

            //Если нет разрешенных переходов выходим
            if (allAllowTransition.Count == 0)
            {
                return;
            }

            //Проходимся по всем разрешенным переходам
            foreach (var transition in allAllowTransition)
            {
                var child = ExecuteTransition(tree.data.NetState, (Transition)transition);

                foreach (var n in nodes)
                {
                    if (Enumerable.SequenceEqual(n.MNet, child.data.MNet))
                    {
                        return;
                    }
                }

                nodes.Add(child.data);

                tree.AddChild(child);

                //if (nodes.Count == 0)
                //{
                //    nodes.Add(child.data);
                //}

                //foreach (var n in nodes.ToList())
                //{
                //    if (child.data.MNet != n.MNet)
                //    {
                //        tree.AddChild(child);
                //        nodes.Add(child.data);
                //    }
                //}


                TreeBuilding(child, nodes);
            }
        }

        public NTree<NodeTree> ExecuteTransition(Model model, Transition transition)
        {
            var child = CreateNodeTree(model);

            foreach (var item in child.data.NetState)
            {
                if (item is Transition)
                {
                    var trans = (Transition)item;

                    if (trans.Id == transition.Id)
                    {
                        OneStepExecuteNet((Transition)item);
                        child.data.MNet = M(child.data.NetState);
                    }
                }
            }

            return child;
        }

        //    /// <summary>
        //    /// Щаг для расчета листа
        //    /// </summary>
        //    /// <param name="parent">Родительский лист</param>
        //    /// <param name="model">Состояние сети в данный шаг</param>
        //    public void Step(Tree tree)
        //    {
        //        var allAllowTransition = AllAllowTransitionOnStep(tree.Node.Model);

        //        //Если нет разрешенных переходов выходим
        //        if (allAllowTransition.Count == 0)
        //        {
        //            return;
        //        }

        //        //Проходимся по всем разрешенным переходам
        //        foreach (var transition in allAllowTransition)
        //        {
        //            tree.Child = CreateNode(tree,);
        //        }
        //    }
        //}

        //public enum State
        //{
        //    Deadlock,
        //    Border,
        //    Terminal
        //}
    }
}
