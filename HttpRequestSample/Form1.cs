using HttpRequestSample.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using HttpRequestSample.Request;

namespace HttpRequestSample
{
    public partial class Form1 : Form
    {

        ProductRequest productRequest = new ProductRequest();
        List<Product> products = new List<Product>();
        Product productClicked = null;



        public Form1()
        {
            InitializeComponent();

            hideProgressBar();
        }


        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                await renderProductsIntoGridView();
               
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void resetForm()
        {
            textBoxName.Text = "";
            textBoxPrice.Text = "";
            productClicked = null;
        }

        private async Task renderProductsIntoGridView()
        {
           try
           {
                showProgressBar();
                products = await productRequest.getProductsAsync();
                dataGridView_products.DataSource = products;
           }
           catch (Exception ex)
           {
                MessageBox.Show(ex.Message);
           } finally
           {
                hideProgressBar();
           }
        }


        // Add new prdoduct
        private async void buttonAddProduct_Click(object sender, EventArgs e)
        {
            Product product = new Product();

            // Generate random id
            Random random = new Random();
            product.Id = random.Next(1, 1000);

            product.Name = textBoxName.Text;
            product.Price = Convert.ToDouble(textBoxPrice.Text);

            try
            {
                showProgressBar();
                bool result = await productRequest.createProductAsync(product);

                // Re-Render products
                if (result)
                {
                    await renderProductsIntoGridView();
                } else
                {
                    MessageBox.Show("Failed to add product");
                }
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


        // Update product
        private void dataGridView_products_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_products.CurrentCell.RowIndex != -1)
            {
                int currentIdClicked = Convert.ToInt32(dataGridView_products.Rows[e.RowIndex].Cells["Id"].Value);


                // Tìm sản phẩm trong List products theo Id đã clicked
                productClicked = products.Find(p => p.Id == currentIdClicked);

                textBoxName.Text = productClicked.Name;
                textBoxPrice.Text = productClicked.Price.ToString();
            }
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                showProgressBar();

                productClicked.Name = textBoxName.Text;
                productClicked.Price = Convert.ToDouble(textBoxPrice.Text);

                bool result = await productRequest.updateProductAsync(productClicked);

                if (result)
                {
                    await renderProductsIntoGridView();
                    resetForm();
                }
                else
                {
                    MessageBox.Show("Failed to update product");
                }

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } finally
            {
                hideProgressBar();
            }
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                showProgressBar();

                bool result = await productRequest.deleteProductAsync(productClicked.Id);

                if (result)
                {
                    await renderProductsIntoGridView();
                    resetForm();
                }
                else
                {
                    MessageBox.Show("Failed to delete product");
                }

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
    }
}
