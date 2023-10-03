using Ordenes.Acceso_a_Datos;
using Ordenes.Acceso_a_Datos.implementacion;
using Ordenes.Entidades;
using Ordenes.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Ordenes
{
    public partial class FrmOrdenRetiro : Form
    {
        private GestorMaterial GestorMaterial;
        GestorOrden GestorOrden; 
        OrdenRetiro NvaOrden;
        public FrmOrdenRetiro()
        {
            InitializeComponent();
            GestorMaterial = new GestorMaterial(new DaoFactory().CrearMaterial());
            GestorOrden =new GestorOrden(new DaoFactory());
            NvaOrden = new OrdenRetiro();
        }

        private void FrmOrdenRetiro_Load(object sender, EventArgs e)
        {
            cargarMateriales();
            cboMaterial.SelectedIndex = -1;
        }

        private void cargarMateriales()
        {
            cboMaterial.DataSource = GestorMaterial.TrearMateriales();
            cboMaterial.DisplayMember = "nombre";
            cboMaterial.ValueMember = "codigo";

        }

        private void cboMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCantidad.Text) || int.Parse(txtCantidad.Text.ToString())== 0)
            {
                MessageBox.Show("INGRESE UNA CANTIDAD", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtResponsable.Text))
            {
                {
                    MessageBox.Show("INGRESE UN RESPONSABLE" , "ATENCION" , MessageBoxButtons.OK , MessageBoxIcon.Warning);
                    return;
                }
            }
            if (cboMaterial.SelectedIndex == -1)
            {
                MessageBox.Show("SELECCIONE UN MATERIAL", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (DataGridViewRow row in dgvOrdenes.Rows)
            {
                if (row.Cells["ColMaterial"].Value.ToString() == cboMaterial.Text)
                {
                    MessageBox.Show("EL MATERIAL "+ "'' " +cboMaterial.Text + "'' " + "YA FUE CARGADO " , "ATENCION" ,MessageBoxButtons.OK , MessageBoxIcon.Warning );
                    return;
                }
            }
            Material m = (Material)cboMaterial.SelectedItem;

            int cantidad = Convert.ToInt32(txtCantidad.Text);

            DetalleOrden detalle = new DetalleOrden(m ,cantidad );

            if(cantidad <= m.Stock)
            {
                //asigno la fecha a la orden
                DateTime fecha = dtpFecha.Value;
                string respon = txtResponsable.Text.ToString();

                NvaOrden.AgregarDetalle(detalle);

                dgvOrdenes.Rows.Add(new object[] { m.Codigo, m.Nombre, m.Stock, cantidad, "Quitar" });
            }
            else
            {
                MessageBox.Show("LA CANTIDAD PEDIDA , EXCEDE A LA CANTIDAD EN STOCK" , "ERROR" , MessageBoxButtons.OK , MessageBoxIcon.Error);
            }

        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                MessageBox.Show("INGRESE UNA CANTIDAD NUMERICA" , "ERROR", MessageBoxButtons.OK , MessageBoxIcon.Error);
                e.Handled = true;
            }
        }

        private void dgvOrdenes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvOrdenes.CurrentCell.ColumnIndex ==4)
            {
                NvaOrden.EliminarDetalle(dgvOrdenes.CurrentRow.Index);
                dgvOrdenes.Rows.RemoveAt(dgvOrdenes.CurrentRow.Index);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            NvaOrden.Responsable = txtResponsable.Text.ToString();
            NvaOrden.Fecha = dtpFecha.Value;
            
            if (dgvOrdenes.Rows.Count == 0)
            {
                MessageBox.Show("LA GRILLA DEBE CONTENER AL MENOS UN ITEM" , "ERROR" , MessageBoxButtons.OK , MessageBoxIcon.Error);
            }  

            if(GestorOrden.crearOrden(NvaOrden)== true)
            {
                MessageBox.Show("ORDEN CARGADA CORRECTAMENTE" , "CONFIRMACION" , MessageBoxButtons.OK , MessageBoxIcon.Information) ;
                EliminarCampos();
            }
            else
            {
                MessageBox.Show("no se registro correctamente");
            }
        }

        private void txtResponsable_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboMaterial_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txtResponsable_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar))
            { 
                MessageBox.Show("DEBE INGRESAR UN RESPONSABLE VALIDO", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }

        public void EliminarCampos()
        {
            txtCantidad.Text = string.Empty;
            txtResponsable.Text = string.Empty;
            cboMaterial.SelectedIndex = -1;
            dgvOrdenes.Rows.Clear();
            dtpFecha.Value = DateTime.Today;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            EliminarCampos();
        }
    }
}
