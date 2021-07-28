using Petri_Nets.Controls;
using Petri_Nets.Core;
using Petri_Nets.Models;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;

namespace Petri_Nets
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Модель всей сети Петри
        /// </summary>
        Model model = new Model();

        /// <summary>
        /// Модель для рассчетов
        /// </summary>
        Model simulation = new Model();

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer time = new System.Windows.Forms.Timer();

        /// <summary>
        /// Время начала запуска симуляции
        /// </summary>
        private DateTime startTime;

        int step = 0;

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

        // This is the method to run when the timer is raised.
        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            stepScreen.Text = Convert.ToString(++step);

            Helper.Shuffle(simulation);

            foreach (Transition transition in simulation)
            {
                if (transition.Previous.Count == 0)
                {
                    continue;
                }

                var allowTransiton = CheckTransition(transition);

                //var fake = transition.Copy();

                ////данные для посчета фишек для разрешающего перехода
                //var countLink = 0;
                //var countTokens = 0;

                //foreach (var linked in fake.Previous)
                //{
                //    countLink++;

                //    var place = (Place)linked;

                //    if (place.Tokens > 0)
                //    {
                //        countTokens++;
                //        place.Tokens--;
                //    }
                //}

                if (allowTransiton == true)
                {
                    transition.AllowTransition = true;
                    transition.CountTransited++;
                    dragPanel.Refresh();
                    Thread.Sleep(1000);

                    transition.Previous.ForEach(Sub);
                    dragPanel.Refresh();
                    Thread.Sleep(1000);

                    transition.AllowTransition = false;

                    transition.Next.ForEach(Add);
                    dragPanel.Refresh();
                    Thread.Sleep(1000);
                }
                else
                {
                    transition.AllowTransition = false;
                }
            }

            dragPanel.Refresh();
        }

        private bool CheckTransition(Transition transition)
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

        private void CreateTree()
        {

        }

        // This is the method to run when the timer is raised.
        //private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        //{
        //    stepScreen.Text = Convert.ToString(++step);

        //    foreach (Transition transition in simulation)
        //    {
        //        if (transition.Previous.Count == 0)
        //        {
        //            continue;
        //        }

        //        var fake = transition.Copy();

        //        //данные для посчета фишек для разрешающего перехода
        //        var countLink = 0;
        //        var countTokens = 0;

        //        foreach (var linked in fake.Previous)
        //        {
        //            countLink++;

        //            var place = (Place)linked;

        //            if (place.Tokens > 0)
        //            {
        //                countTokens++;
        //                place.Tokens--;
        //            }
        //        }

        //        if (countTokens >= countLink && transition.Next.Count != 0)
        //        {
        //            transition.AllowTransition = true;
        //            transition.CountTransited++;
        //            dragPanel.Refresh();
        //            Thread.Sleep(1000);

        //            transition.Previous.ForEach(Sub);
        //            dragPanel.Refresh();
        //            Thread.Sleep(1000);

        //            transition.AllowTransition = false;

        //            transition.Next.ForEach(Add);
        //            dragPanel.Refresh();
        //            Thread.Sleep(1000);
        //        }
        //        else
        //        {
        //            transition.AllowTransition = false;
        //        }
        //    }

        //    dragPanel.Refresh();
        //}

        //private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        //{
        //    stepScreen.Text = Convert.ToString(++step);

        //    foreach (var node in model)
        //    {
        //        if (node is Transition)
        //        {
        //            ff.Add(node);

        //            var transition = (Transition)node;

        //            if (transition.Previous.Count == 0)
        //            {
        //                continue;
        //            }

        //            var fake = transition.Copy();

        //            //данные для посчета фишек для разрешающего перехода
        //            var countLink = 0;
        //            var countTokens = 0;

        //            foreach (var linked in fake.Previous)
        //            {
        //                countLink++;

        //                var place = (Place)linked;

        //                if (place.Tokens > 0)
        //                {
        //                    countTokens++;
        //                    place.Tokens--;
        //                }
        //            }

        //            if (countTokens >= countLink && transition.Next.Count != 0)
        //            {
        //                transition.AllowTransition = true;
        //                transition.CountTransited++;
        //                dragPanel.Refresh();
        //                Thread.Sleep(1000);

        //                transition.Previous.ForEach(Sub);
        //                dragPanel.Refresh();
        //                Thread.Sleep(1000);

        //                transition.AllowTransition = false;

        //                transition.Next.ForEach(Add);
        //                dragPanel.Refresh();
        //                Thread.Sleep(1000);
        //            }
        //            else
        //            {
        //                transition.AllowTransition = false;
        //            }
        //        }
        //    }

        //    dragPanel.Refresh();
        //}

        public MainForm()
        {
            //Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru-RU");
            InitializeComponent();
            SetMenuItemEvents();
            SetTimer();
            Time();

            model.Add(new Place(model)
            {
                Tag = "P1",
                Tokens = 1
            });

            model.Add(new Place(model)
            {
                Tag = "P2",
                Tokens = 1
            });

            model.Add(new Place(model)
            {
                Tag = "P3",
                Tokens = 1
            });
            model.Add(new Place(model)
            {
                Tag = "P4",
                Tokens = 1
            });
            model.Add(new Place(model)
            {
                Tag = "P5",
                Tokens = 0
            });
            model.Add(new Place(model)
            {
                Tag = "P6",
                Tokens = 0
            });
            model.Add(new Place(model)
            {
                Tag = "P7",
                Tokens = 0
            });
            model.Add(new Place(model)
            {
                Tag = "P8",
                Tokens = 0
            });
            model.Add(new Place(model)
            {
                Tag = "P9",
                Tokens = 0
            });

            model.Add(new Transition(model)
            {
                Tag = "T1",
                Priority = 1
            });

            
            model.Add(new Transition(model)
            {
                Tag = "T2",
                Priority = 2
            });

            model.Add(new Transition(model)
            {
                Tag = "T3",
                Priority = 3
            });
            model.Add(new Transition(model)
            {
                Tag = "T4",
                Priority = 1
            });


            model.Add(new Transition(model)
            {
                Tag = "T5",
                Priority = 2
            });

            model.Add(new Transition(model)
            {
                Tag = "T6",
                Priority = 3
            });
            model.Add(new Transition(model)
            {
                Tag = "T7",
                Priority = 3
            });


            dfdf();

            dragPanel.Build(model);
        }

        private void dfdf()
        {
            foreach (var node in model)
            {
                if (node is Transition)
                {
                    simulation.Add(node);
                }
            }
        }

        private void SetTimer()
        {
            timer.Tick += new EventHandler(TimerEventProcessor);

            timer.Interval = 1000;
        }

        private void Time()
        {
            time.Interval = 1;
            time.Tick += new EventHandler(TimeTick);
        }

        private void TimeTick(Object obj, EventArgs eventArgs)
        {
            TimeSpan ts = DateTime.Now - startTime;
            timeScreen.Text = String.Format("{0}:{1}:{2}:{3}:{4}", ts.Days, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
        }

        /// <summary>
        /// Создание нового проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            model.Clear();
            dragPanel.Refresh();
        }

        /// <summary>
        /// Сохранить проект в выбранном формате
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog { Filter = "Schema|*.schema" };
            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                using (var fs = File.Create(sfd.FileName))
                {
                    new BinaryFormatter().Serialize(fs, model);
                }
            }
        }

        /// <summary>
        /// Открытие проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = "Schema|*.schema" };
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                using (var fs = File.OpenRead(ofd.FileName))
                {
                    model = (Model)new BinaryFormatter().Deserialize(fs);
                    dragPanel.Build(model);
                    dragPanel.Refresh();
                }
            }
        }

        void SetMenuItemEvents()
        {
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            //runNetStripMenuItem.Click += runNetStripMenuItem_Click;
            stopStripMenuItem.Click += stopStripMenuItem_Click;
            resetStripMenuItem.Click += resetStripMenuItem_Click;
            addPlaceStripMenuItem.Click += addPlaceStripMenuItem_Click;
            addTrantisionStripMenuItem.Click += addTrantisionStripMenuItem_Click;
            deleteNodeStripMenuItem.Click += deleteNodeStripMenuItem_Click;
            optionsToolStripMenuItem.Click += optionsToolStripMenuItem_Click;

            runRandomStripMenuItem.Click += runRandomStripMenuItem_Click;
            runPrioritylStripMenuItem.Click += runPrioritylStripMenuItem_Click;
        }

        private void runPrioritylStripMenuItem_Click(object sender, EventArgs e)
        {
            //var mySortedList = ff.OrderBy(x => ((Transition)x).Priority);

            var simulationOrder = simulation.OrderBy(x => ((Transition)x).Priority).ToList();

            simulation = new Model();
            simulation.AddRange(simulationOrder);

            timer.Start();
            time.Start();
            startTime = DateTime.Now;
        }

        private void runRandomStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.Shuffle(model);
            timer.Start();
            time.Start();
            startTime = DateTime.Now;
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var optionsForm = new Options();

            optionsForm.Show();
        }

        private void deleteNodeStripMenuItem_Click(object sender, EventArgs e)
        {
            dragPanel.RemoveSelected();
            dragPanel.Refresh();
        }

        /// <summary>
        /// Добавление нового элемента сети - переход
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addTrantisionStripMenuItem_Click(object sender, EventArgs e)
        {
            var newTransition = new TransitionForm();

            var result = newTransition.ShowDialog();

            if (result == DialogResult.OK)
            {
                model.Add(new Transition(model)
                {
                    Tag = newTransition.nameTransition.Text,
                    AllowTransition = newTransition.allowTransition.Checked,
                    Location = new Point((int)newTransition.xLocation.Value, (int)newTransition.yLocation.Value),
                    Priority = (int)newTransition.PriorityNumber.Value
                });

                dragPanel.Refresh();
            }
        }

        /// <summary>
        /// Добавление нового элемента сети - позиция
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addPlaceStripMenuItem_Click(object sender, EventArgs e)
        {
            var newPlace = new PlaceForm();

            var result = newPlace.ShowDialog();

            if (result == DialogResult.OK)
            {
                model.Add(new Place(model)
                {
                    Tag = newPlace.namePlace.Text,
                    Tokens = (int)newPlace.countTokens.Value,
                    Location = new Point((int)newPlace.xLocation.Value, (int)newPlace.yLocation.Value)
                });

                dragPanel.Refresh();
            }
        }

        /// <summary>
        /// Сбрасываем установки сети и таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetStripMenuItem_Click(object sender, EventArgs e)
        {
            //time = 1;
        }

        /// <summary>
        /// Останавливаем работу выполнения сети
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Stop();
            time.Stop();
        }

        private void runNetStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Start();
            time.Start();
            startTime = DateTime.Now;
        }

        /// <summary>
        /// Exit from application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Helper.Shuffle(model);
        }



        Net analysis = new Net();

        /// <summary>
        /// Анализ сети
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            analysis.AnalysisNet(model);

        }
    }
}