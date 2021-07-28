using System;
using System.Collections.Generic;
using System.Text;

namespace Petri_Nets.Models
{
    /// <summary>
    /// Класс статистики элементов
    /// </summary>
    public class Stats
    {
        /// <summary>
        /// Максимальное количество фишек в позиции
        /// </summary>
        public int MaxTokens { get; set; }

        /// <summary>
        /// Количество срабатывания перехода
        /// </summary>
        public int CountTransition { get; set; }
    }
}
