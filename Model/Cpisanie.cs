//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Polimer.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cpisanie
    {
        public int IdCpisanie { get; set; }
        public int IdCtelag { get; set; }
        public int НомерДокумента { get; set; }
        public decimal Количество { get; set; }
    
        public virtual Ctelag Ctelag { get; set; }
    }
}