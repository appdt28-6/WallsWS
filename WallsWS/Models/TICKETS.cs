
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
    
public partial class TICKETS
{

    public int Ticket_Id { get; set; }

    public int Sucu_Id { get; set; }

    public int barb_id { get; set; }

    public System.DateTime Ticket_Date { get; set; }

    public double Ticket_Subtotal { get; set; }

    public int Ticket_Factura { get; set; }

    public string Ticket_Status { get; set; }

    public Nullable<int> Ticket_Pago { get; set; }

    public Nullable<int> Ticket_Turno { get; set; }

}

}
