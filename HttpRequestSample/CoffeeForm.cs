using HttpRequestSample.Model;
using HttpRequestSample.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HttpRequestSample
{
    public partial class CoffeeForm : Form
    {

        CoffeeProductRequest productRequest = new CoffeeProductRequest();

        public CoffeeForm()
        {
            InitializeComponent();
        }

        private void showProgressBar()
        {
            progressBarLoading.Visible = true;
            progressBarLoading.Style = ProgressBarStyle.Marquee; // Dùng Marquee để hiển thị tiến trình vô hạn
            progressBarLoading.MarqueeAnimationSpeed = 30; // Tốc độ chuyển động của Marquee
        }

        private void hideProgressBar()
        {
            progressBarLoading.Visible = false;
        }


        private async Task renderProductsIntoGridView()
        {
            try
            {
                showProgressBar();
                List<CoffeeProduct> products = await productRequest.getCoffeeProductsAsync();
                dataGridView_products.DataSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                hideProgressBar();
            }
        }

        private async void CoffeeForm_Load(object sender, EventArgs e)
        {
            try
            {
                await renderProductsIntoGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
