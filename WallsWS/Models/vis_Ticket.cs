
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------


namespace WallsWS.Models
{

using System;
    using System.Collections.Generic;
    
public partial class vis_Ticket
{

    public int ticket_id { get; set; }

    public int sucu_id { get; set; }

    public int barb_id { get; set; }

    public string barb_name { get; set; }

    public System.DateTime ticket_date { get; set; }

    public double ticket_subtotal { get; set; }

    public string Ticket_Status { get; set; }

    public Nullable<int> Ticket_Pago { get; set; }

}

}
