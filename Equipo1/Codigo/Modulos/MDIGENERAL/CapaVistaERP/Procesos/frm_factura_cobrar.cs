﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaControladorERP;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CapaVistaERP.Procesos
{
    //David Carrillo 0901-20-3201
    public partial class frm_factura_cobrar : Form
    {
        Controlador cn=new Controlador();
        public frm_factura_cobrar()
        {
            InitializeComponent();
            dgvFactura.Format = DateTimePickerFormat.Custom;
            dgvFactura.CustomFormat = "yyyy-MM-dd";
            dgvVencimiento.Format = DateTimePickerFormat.Custom;
            dgvVencimiento.CustomFormat = "yyyy-MM-dd";
            diasEnpagar();


            /* DateTime fechaSeleccionada = dgvFactura.Value;
             DateTime nuevaFecha = fechaSeleccionada.AddDays(15);
             dgvVencimiento.Value = nuevaFecha;*/
            cmb_diaspagar.SelectedIndexChanged += cmb_diaspagar_SelectedIndexChanged;


            UltimaFact();
        }


        public void diasEnpagar()
        {
            cmb_diaspagar.Items.Add("7");
            cmb_diaspagar.Items.Add("15");
            cmb_diaspagar.Items.Add("22");
        }


        public void fechaVencimiento()
        {
            DateTime fechaSeleccionada = dgvFactura.Value;
            int diasAPagar = 0;
            if (int.TryParse(cmb_diaspagar.SelectedItem?.ToString(), out diasAPagar))
            {
                DateTime nuevaFecha = fechaSeleccionada.AddDays(diasAPagar);
                dgvVencimiento.Value = nuevaFecha;
            }
            else
            {
                MessageBox.Show("error");
            }
        }
        private void obtenerNoFact()
        {
            string ped= txt_numPedido.Text;
            DataTable facno = cn.Buscar("tbl_facturaxcobrar", "tbl_Ventas_detalle_id_ventas_det", ped);
            if (facno.Rows.Count > 0)
            {

                DataRow row = facno.Rows[0];
               txt_numfactura.Text = row["NoFactura"].ToString();
                MessageBox.Show("numFact", txt_numfactura.Text = row["NoFactura"].ToString());
            }

        }
        private void UltimaFact()
        {
            string idFact = cn.ObtenerUltimoDato("NoFactura", "tbl_facturaxcobrar", "NoFactura");
            if (idFact == "No hay dato registrado")
            {
                idFact = "0";
                txt_numfactura.Text = idFact;
            }
            else
            {
                int Fact= Convert.ToInt32(idFact) ;
                Console.WriteLine("NFactura " + Fact);
                txt_numfactura.Text = Fact.ToString();
                Console.WriteLine("id ultima Factura " + idFact);
            }
        }

        private void btn_Buscar_Click(object sender, EventArgs e)
        {
            int idcoti;
            if (!int.TryParse(txt_numcoti.Text, out idcoti))
            {
                MessageBox.Show("Ingrese un ID  válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Creditos a Carlos Guzman
            buscar(idcoti);
        }

        private void buscar(int idcoti)
        {
           
            DataTable dt = cn.Buscar("tbl_ventaspedido", "tbl_cotizaciones_No_Cotizacion", idcoti.ToString());
            
            if (dt.Rows.Count > 0)
            {

                DataRow row = dt.Rows[0];
                txt_numPedido.Text = row["id_ventas_ped"].ToString();
                txt_vend.Text= row["id_vendedor"].ToString();
                DataTable nombre_vend = cn.BuscarDato("nombre_vend", "tbl_vendedor", "id_vendedor", Convert.ToInt32(txt_vend.Text));
                if (nombre_vend.Rows.Count > 0)
                {
                    txt_vend.Text = nombre_vend.Rows[0]["nombre_vend"].ToString();
                }
              
            }
            else
            {
                MessageBox.Show(" no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            string ped = txt_numPedido.Text;
            MessageBox.Show("pedido " + ped);
            /* DataTable facno = cn.Buscar("tbl_facturaxcobrar", "tbl_Ventas_detalle_id_ventas_det", ped);
             if (facno.Rows.Count > 0)
             {

                 DataRow row = facno.Rows[0];
                 txt_numfactura.Text = row["NoFactura"].ToString();
                 MessageBox.Show("numFact", txt_numfactura.Text = row["NoFactura"].ToString());
             }
             else
             {
                 MessageBox.Show("nnno hay nada");
             }*/


            /*
            int idFact = Convert.ToInt32(txt_numfactura.Text);
            DataTable dt2 = cn.Buscar("tbl_facturaxcobrar", "NoFactura", idFact.ToString());
            if (dt2.Rows.Count > 0)
            {

                DataRow row = dt2.Rows[0];
                txt_total.Text = row["total_facxcob"].ToString();
                dgvVencimiento.Text = row["tiempoPago_facxcob"].ToString();
                txt_facturaestado.Text = row["estado_facxcob"].ToString();
                txt_nombrecl.Text = row["tbl_Clientes_id_cliente"].ToString();
                txt_idcliente.Text = txt_nombrecl.Text;
               DataTable nombre_cl = cn.BuscarDato("nombre_cl", "tbl_clientes", "id_cliente", Convert.ToInt32(txt_nombrecl.Text));
                if (nombre_cl.Rows.Count > 0)
                {
                    txt_nombrecl.Text = nombre_cl.Rows[0]["nombre_cl"].ToString();
                }
              
            }
            else
            {
                MessageBox.Show(" no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/

            string idCoti = txt_numcoti.Text;
            DataTable detalleCoti = cn.Buscar("tbl_detalle_cotizacion", "tbl_cotizaciones_No_Cotizacion", idCoti);
            if (detalleCoti.Rows.Count > 0)
            {

                DataRow row = detalleCoti.Rows[0];
                txt_total.Text = row["total_detCoti"].ToString();
                txt_facturaestado.Text = "Por Pagar";
                txt_nombrecl.Text = row["tbl_Clientes_id_cliente"].ToString();
                txt_idcliente.Text = txt_nombrecl.Text;
                DataTable nombre_cl = cn.BuscarDato("nombre_cl", "tbl_clientes", "id_cliente", Convert.ToInt32(txt_nombrecl.Text));
                if (nombre_cl.Rows.Count > 0)
                {
                    txt_nombrecl.Text = nombre_cl.Rows[0]["nombre_cl"].ToString();
                }

            }
            else
            {
                MessageBox.Show(" no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            int idcl = Convert.ToInt32(txt_idcliente.Text);
            DataTable dt3 = cn.Buscar("tbl_clientes", "id_cliente", idcl.ToString());
            if (dt3.Rows.Count > 0)
            {

                DataRow row = dt3.Rows[0];
                txt_apellidocl.Text = row["apellido_cl"].ToString();
                txt_direccioncl.Text = row["direccion_cl"].ToString();
                txt_coreocl.Text = row["correo_cl"].ToString();
                txt_telefonocl.Text = row["telefono_cl"].ToString();

               
                
            }
            else
            {
                MessageBox.Show(" no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            string coti = txt_numcoti.Text;
            //Metodo buscar creditos a Carlos Guzman

            DataTable detalleCotis = cn.Buscar("tbl_detalle_cotizacion", "tbl_cotizaciones_No_Cotizacion", coti);


            dgv_detalle.Rows.Clear();



            if (dgv_detalle.Columns.Count == 0)
            {
                foreach (DataColumn column in detalleCoti.Columns)
                {
                    if (column.ColumnName != "id_detalle_cotizacion" &&
                        column.ColumnName != "tbl_clientes_id_cliente" &&
                        column.ColumnName != "tbl_cotizaciones_No_Cotizacion")
                    {
                        dgv_detalle.Columns.Add(column.ColumnName, column.ColumnName);
                    }
                }
            }



            if (detalleCotis.Rows.Count > 0)
            {
                foreach (DataRow row in detalleCotis.Rows)
                {
                    List<object> rowData = new List<object>();

                    foreach (DataColumn column in detalleCoti.Columns)
                    {
                        if (column.ColumnName != "id_detalle_cotizacion" &&
                            column.ColumnName != "tbl_clientes_id_cliente" &&
                            column.ColumnName != "tbl_cotizaciones_No_Cotizacion")
                        {

                            if (column.ColumnName == "total_detCoti")
                            {
                                int productId = Convert.ToInt32(row["tbl_producto_cod_producto"]);

                                DataTable result = cn.BuscarDato("precioUnitario_prod", "tbl_producto", "cod_producto", productId);
                                double precioUnitario = 0.0; 

                                if (result.Rows.Count > 0)
                                {
                                    precioUnitario = Convert.ToDouble(result.Rows[0]["precioUnitario_prod"]);
                                }

                                rowData.Add(precioUnitario);
                            }

                            if (column.ColumnName == "tbl_producto_cod_producto")
                            {
                                int productId = Convert.ToInt32(row["tbl_producto_cod_producto"]);
                                DataTable result = cn.BuscarDato("nombre_prod", "tbl_producto", "cod_producto", productId);
                                string nombreProd = string.Empty;
                                if (result.Rows.Count > 0)
                                {
                                    nombreProd = result.Rows[0]["nombre_prod"].ToString();
                                }

                                rowData.Add(nombreProd);
                            }
                            else
                            {
                                rowData.Add(row[column.ColumnName]);
                            }
             

                        }
                    }
                    dgv_detalle.Rows.Add(rowData.ToArray());
                }
            }

           
        }

        private void btn_Pagar_Click(object sender, EventArgs e)
        {
            double total = Convert.ToDouble(txt_total.Text);
            string limite = dgvVencimiento.Text;
            string estado = txt_facturaestado.Text;
            int idVenta = Convert.ToInt32(txt_numPedido.Text);
            int cl = Convert.ToInt32(txt_idcliente.Text);

            cn.InsertarFactura(total, limite, estado, idVenta, cl);

            MessageBox.Show("Factura Guardada con exito");
        }

        private void cmb_diaspagar_SelectedIndexChanged(object sender, EventArgs e)
        {
            fechaVencimiento();
        }
    }
}
