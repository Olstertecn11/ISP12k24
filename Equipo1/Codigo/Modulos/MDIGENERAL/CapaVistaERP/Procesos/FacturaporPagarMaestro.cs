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

//Programado por: Carol Chuy 
namespace CapaVistaERP.Procesos
{
    public partial class FacturaporPagarMaestro : Form
    {
        private FacturaporPagarEli FacturaporPagarEli;
        private FacturaporPagarModi FacturaporPagarModi;
        private string idcompra = "";
        private string fechaV = "";
        private string proveedorfact = "";
        Controlador cn = new Controlador();
        public FacturaporPagarMaestro(FacturaporPagarEli facturaporPagar, FacturaporPagarModi facturaporPagarModi)
        {
            InitializeComponent();
            actualizardatagridview();
            FacturaporPagarEli = facturaporPagar;
            FacturaporPagarModi = facturaporPagarModi;
        }

        private void btn_nueva_Click(object sender, EventArgs e)
        {
            FacturaporPagar FacturaporPagar = new FacturaporPagar(idcompra, fechaV, proveedorfact);
            FacturaporPagar.Show();
        }
        public void actualizardatagridview()
        {
            DataTable dt = cn.llenarTablas("tbl_facturaxpagar");
            dt_datos.DataSource = dt;
        }

        private void btn_refrescar_Click(object sender, EventArgs e)
        {
            actualizardatagridview();
        }

        private void btn_salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cb_filtro_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el índice de la opción seleccionada
            int seleccion = cb_filtro.SelectedIndex;

            // Ejecutar diferentes acciones según el índice seleccionado
            switch (seleccion)
            {
                case 0:
                    int añoSeleccionado = Convert.ToInt32(cb_año.SelectedItem);
                    DataTable dtRegistros = cn.ObtenerFacturasPorFecha(añoSeleccionado, "Diario");
                    // Asignar el DataTable al DataGridView para mostrar los registros filtrados
                    dt_datos.DataSource = dtRegistros;
                    break;
                case 1:
                    int añoSeleccionado1 = Convert.ToInt32(cb_año.SelectedItem);
                    DataTable dtRegistros1 = cn.ObtenerFacturasPorFecha(añoSeleccionado1, "Semanal");
                    // Asignar el DataTable al DataGridView para mostrar los registros filtrados
                    dt_datos.DataSource = dtRegistros1;
                    break;
                case 2:
                    int añoSeleccionado2 = Convert.ToInt32(cb_año.SelectedItem);
                    DataTable dtRegistros2 = cn.ObtenerFacturasPorFecha(añoSeleccionado2, "Mensual");
                    // Asignar el DataTable al DataGridView para mostrar los registros filtrados
                    dt_datos.DataSource = dtRegistros2;
                    break;
                default:
                    break;
            }
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            if (dt_datos.SelectedRows.Count > 0)
            {
                // Obtener la primera celda de la fila seleccionada (que corresponde a la primera columna)
                DataGridViewCell firstCell = dt_datos.SelectedRows[0].Cells[0];
                string valorPrimeraColumna = firstCell.Value.ToString();
                FacturaporPagarEli.RecibirDatosDesdeFacturasEli(valorPrimeraColumna);
            }
            else
            {
                MessageBox.Show("Seleccione una fila.", "Selección de Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_modificar_Click(object sender, EventArgs e)
        {
            if (dt_datos.SelectedRows.Count > 0)
            {
                // Obtener la primera celda de la fila seleccionada (que corresponde a la primera columna)
                DataGridViewCell firstCell = dt_datos.SelectedRows[0].Cells[0];
                string valorPrimeraColumna = firstCell.Value.ToString();
                FacturaporPagarModi.RecibirDatosDesdeFacturaModi(valorPrimeraColumna);
            }
            else
            {
                MessageBox.Show("Seleccione una fila.", "Selcción Orden", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
