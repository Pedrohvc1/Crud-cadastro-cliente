using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // name_space SQLServer

namespace Cadastro_Cliente
{
    public partial class Form1 : Form
    {
        SqlConnection conexao = null;
        SqlCommand comando;
        SqlDataAdapter dataAdapter;
        SqlDataReader dataReader;

        string strSQL;

        public Form1()
        {
            InitializeComponent();
        }


        private void btnNovo_Click(object sender, EventArgs e) // inserir dados
        {

            conexao = new SqlConnection(@"Server=PEDRO\SQLEXPRESS; Database=Cliente; Trusted_Connection=True"); // User Id=sa; Password=Pe.9150;

            
            strSQL = "INSERT INTO Cadastro_Cliente (Nome, Data_nascimento, Telefone, Endereco, Email) " +
                "VALUES (@Nome, @Data_nascimento, @Telefone, @Endereco, @Email)";

            comando = new SqlCommand(strSQL, conexao);
                        
            comando.Parameters.AddWithValue("@Nome", txtNome.Text);
            comando.Parameters.AddWithValue("@Data_nascimento", mskDataNascimento.Text);
            comando.Parameters.AddWithValue("@Telefone", mskTelefone.Text);
            comando.Parameters.AddWithValue("@Endereco", txtEndereco.Text);
            comando.Parameters.AddWithValue("@Email", txtEmail.Text);

            try
            {
                conexao.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Cadastro realizado com sucesso!");                
            }

            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                conexao.Close();
            }
            
        }

        private void btnExibir_Click(object sender, EventArgs e) // exibir os dados
        {
            conexao = new SqlConnection(@"Server=PEDRO\SQLEXPRESS; Database=Cliente; Trusted_Connection=True"); 
            
            strSQL = "SELECT * FROM Cadastro_Cliente"; // select para trazer todos os dados

            DataSet ds = new DataSet();
            dataAdapter = new SqlDataAdapter(strSQL, conexao); //criou um dataAdapter para preencher o DataSet (para exibir no form)                 
           
            try
            {
                conexao.Open(); // abrindo a conexao com o banco
                
                dataAdapter.Fill(ds); // trazendo a consulta e colocando em tabelas dentro do forms
                dgvDados.DataSource = ds.Tables[0];                
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                conexao.Close();
            }

        }

        private void btnConsultar_Click(object sender, EventArgs e) // consultar dados
        {          

            try
            {
                conexao = new SqlConnection(@"Server=PEDRO\SQLEXPRESS; Database=Cliente; Trusted_Connection=True"); // User Id=sa; Password=Pe.9150;


                strSQL = "SELECT * FROM Cadastro_cliente WHERE ID = @ID"; // pelo ID 

                comando = new SqlCommand(strSQL, conexao);

                comando.Parameters.AddWithValue("@ID", txtID.Text);

                if (txtID.Text == string.Empty) // se o campo id for vazio retorna exceção
                {
                    throw new Exception("Você precisa digitar um id");
                }

                conexao.Open();
                dataReader = comando.ExecuteReader();

                if (dataReader.HasRows == false) //caso o id não tenha sido cadastrado // hasRows exceção que retorna nenhum registro
                {
                    throw new Exception("id não cadastrado");
                }

                //txtID.Text = Convert.ToString(dataReader["ID"]);

                while (dataReader.Read())
                {
                    txtNome.Text = Convert.ToString(dataReader["Nome"]); // tem que fazer um cast e passar para string
                    mskDataNascimento.Text = Convert.ToString(dataReader["Data_nascimento"]);
                    mskTelefone.Text = Convert.ToString(dataReader["Telefone"]);
                    txtEndereco.Text = Convert.ToString(dataReader["Endereco"]);
                    txtEmail.Text = Convert.ToString(dataReader["Email"]);
                }

            }

            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e) // editar dados
        {
            conexao = new SqlConnection(@"Server=PEDRO\SQLEXPRESS; Database=Cliente; Trusted_Connection=True"); // User Id=sa; Password=Pe.9150;


            strSQL = "UPDATE Cadastro_Cliente SET Nome = @Nome, Data_nascimento = @Data_nascimento, Telefone = @Telefone," +
                "Endereco = @Endereco, Email = @Email WHERE ID = @ID";

            comando = new SqlCommand(strSQL, conexao);

            comando.Parameters.AddWithValue("@ID", txtID.Text);
            comando.Parameters.AddWithValue("@Nome", txtNome.Text);
            comando.Parameters.AddWithValue("@Data_nascimento", mskDataNascimento.Text);
            comando.Parameters.AddWithValue("@Telefone", mskTelefone.Text);
            comando.Parameters.AddWithValue("@Endereco", txtEndereco.Text);
            comando.Parameters.AddWithValue("@Email", txtEmail.Text);

            try
            {
                conexao.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Edição realizada com sucesso!");
            }

            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                conexao.Close();
            }

        }

        private void btnExcluir_Click(object sender, EventArgs e) // excluir dados
        {
            conexao = new SqlConnection(@"Server=PEDRO\SQLEXPRESS; Database=Cliente; Trusted_Connection=True"); // User Id=sa; Password=Pe.9150;


            strSQL = "DELETE Cadastro_Cliente WHERE ID = @ID";

            comando = new SqlCommand(strSQL, conexao);

            comando.Parameters.AddWithValue("@ID", txtID.Text);
            
            try
            {
                conexao.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Cadastro excluido com sucesso!");
            }

            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                conexao.Close();
            }
        }




















        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
    }
}
