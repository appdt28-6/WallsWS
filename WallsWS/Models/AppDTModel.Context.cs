﻿

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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using System.Data.Entity.Core.Objects;
using System.Linq;


public partial class AppDTEntities : DbContext
{
    public AppDTEntities()
        : base("name=AppDTEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<HORARIO_DISPONIBLE> HORARIO_DISPONIBLE { get; set; }

    public virtual DbSet<SUCURSAL> SUCURSAL { get; set; }

    public virtual DbSet<BARBEROS> BARBEROS { get; set; }

    public virtual DbSet<USUARIOS> USUARIOS { get; set; }

    public virtual DbSet<HORARIO> HORARIO { get; set; }

    public virtual DbSet<TICKETS> TICKETS { get; set; }

    public virtual DbSet<BARBERO_PAGO> BARBERO_PAGO { get; set; }

    public virtual DbSet<vis_Set_Agenda> vis_Set_Agenda { get; set; }

    public virtual DbSet<PRODUCTOS> PRODUCTOS { get; set; }

    public virtual DbSet<vis_AGENDA_BARBEROS> vis_AGENDA_BARBEROS { get; set; }

    public virtual DbSet<BARBERO_COMISION> BARBERO_COMISION { get; set; }

    public virtual DbSet<vis_VENTASTICKET_PRODUCTOS> vis_VENTASTICKET_PRODUCTOS { get; set; }

    public virtual DbSet<VENTASTICKET> VENTASTICKET { get; set; }

    public virtual DbSet<GASTOS> GASTOS { get; set; }

    public virtual DbSet<SERVICIOS> SERVICIOS { get; set; }

    public virtual DbSet<vis_Ticket_Detail> vis_Ticket_Detail { get; set; }

    public virtual DbSet<vis_Ticket> vis_Ticket { get; set; }

    public virtual DbSet<vis_Get_Quanty_Product> vis_Get_Quanty_Product { get; set; }

    public virtual DbSet<WALLSCARD> WALLSCARD { get; set; }

    public virtual DbSet<AGENDA> AGENDA { get; set; }

    public virtual DbSet<vis_AGENDA> vis_AGENDA { get; set; }


    public virtual int sp_SET_HORARIO(Nullable<int> barbid, string agenDate)
    {

        var barbidParameter = barbid.HasValue ?
            new ObjectParameter("barbid", barbid) :
            new ObjectParameter("barbid", typeof(int));


        var agenDateParameter = agenDate != null ?
            new ObjectParameter("agenDate", agenDate) :
            new ObjectParameter("agenDate", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_SET_HORARIO", barbidParameter, agenDateParameter);
    }


    public virtual ObjectResult<sp_Barberos_Pagos_Result> sp_Barberos_Pagos(Nullable<int> barber)
    {

        var barberParameter = barber.HasValue ?
            new ObjectParameter("barber", barber) :
            new ObjectParameter("barber", typeof(int));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Barberos_Pagos_Result>("sp_Barberos_Pagos", barberParameter);
    }


    public virtual ObjectResult<sp_Get_Commission_Result> sp_Get_Commission(Nullable<int> barber)
    {

        var barberParameter = barber.HasValue ?
            new ObjectParameter("barber", barber) :
            new ObjectParameter("barber", typeof(int));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Get_Commission_Result>("sp_Get_Commission", barberParameter);
    }


    public virtual ObjectResult<Nullable<int>> sp_Get_Ventas(Nullable<int> pago)
    {

        var pagoParameter = pago.HasValue ?
            new ObjectParameter("pago", pago) :
            new ObjectParameter("pago", typeof(int));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_Get_Ventas", pagoParameter);
    }


    public virtual ObjectResult<Nullable<int>> sp_Get_Gastos()
    {

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_Get_Gastos");
    }


    public virtual int sp_New_Barber(Nullable<int> barber, Nullable<int> sucur)
    {

        var barberParameter = barber.HasValue ?
            new ObjectParameter("barber", barber) :
            new ObjectParameter("barber", typeof(int));


        var sucurParameter = sucur.HasValue ?
            new ObjectParameter("sucur", sucur) :
            new ObjectParameter("sucur", typeof(int));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_New_Barber", barberParameter, sucurParameter);
    }


    public virtual ObjectResult<sp_SERVICIOS_AGROUP_Result> sp_SERVICIOS_AGROUP(Nullable<int> sucu)
    {

        var sucuParameter = sucu.HasValue ?
            new ObjectParameter("sucu", sucu) :
            new ObjectParameter("sucu", typeof(int));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_SERVICIOS_AGROUP_Result>("sp_SERVICIOS_AGROUP", sucuParameter);
    }


    public virtual ObjectResult<Nullable<int>> sp_Get_Ticktes_BARBEROS(Nullable<int> barber, Nullable<int> sucuid)
    {

        var barberParameter = barber.HasValue ?
            new ObjectParameter("barber", barber) :
            new ObjectParameter("barber", typeof(int));


        var sucuidParameter = sucuid.HasValue ?
            new ObjectParameter("sucuid", sucuid) :
            new ObjectParameter("sucuid", typeof(int));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_Get_Ticktes_BARBEROS", barberParameter, sucuidParameter);
    }

}

}

