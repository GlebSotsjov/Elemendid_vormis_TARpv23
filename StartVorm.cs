namespace Elemendid_vormis_TARpv23
{
    public partial class StartVorm : Form
    {
        List<string> elemendid = new List<string> { "Nupp", "Silt", "Pilt", "Markeruut", "Radionupp", "Tekstikast" };
        List<string> rbtn_list = new List<string> { "Valik 1", "Valik 2", "Valik 3" }; // Названия радиокнопок
        TreeView tree;
        Button btn;
        Label lbl;
        PictureBox pbox;
        RadioButton? rbtn1, rbtn2, rbtn3;
        CheckBox? chk1, chk2;
        TextBox txt;

        public StartVorm()
        {
            this.Height = 500;
            this.Width = 700;
            this.Text = "Vorm elementidega";

            // Создаем и настраиваем TreeView
            tree = new TreeView();
            tree.Dock = DockStyle.Left;
            tree.AfterSelect += Tree_AfterSelect; // Добавляем обработчик события

            // Создаем корневой узел "Elemendid"
            TreeNode tn = new TreeNode("Elemendid: ");
            foreach (var element in elemendid)
            {
                tn.Nodes.Add(new TreeNode(element)); // Добавляем элементы в дерево
            }

            tree.Nodes.Add(tn); // Добавляем корневой узел в дерево
            this.Controls.Add(tree); // Добавляем TreeView на форму

            // Создаем остальные элементы интерфейса
            // Nupp - Button
            btn = new Button();
            btn.Text = "Vajuta siia";
            btn.Height = 50;
            btn.Width = 70;
            btn.Location = new Point(150, 50);
            btn.Click += Btn_Click;

            // Silt - Label
            lbl = new Label();
            lbl.Text = "Aknade elemendid C# abil ";
            lbl.Font = new Font("Arial", 15, FontStyle.Bold);
            lbl.Size = new Size(300, 50);
            lbl.Location = new Point(150, 0);
            lbl.MouseHover += Lbl_MouseHover;
            lbl.MouseLeave += Lbl_MouseLeave;

            // Pilt - PictureBox
            pbox = new PictureBox();
            pbox.Size = new Size(60, 60);
            pbox.Location = new Point(150, btn.Height + btn.Width + 5);
            pbox.SizeMode = PictureBoxSizeMode.Zoom;
            pbox.Image = Image.FromFile(@"..\..\..\Spongebob.png");
            pbox.DoubleClick += Pbox_DoubleClick;

            // Добавляем основные элементы на форму
            this.Controls.Add(btn);
            this.Controls.Add(lbl);
            this.Controls.Add(pbox);
        }

        // Остальной код не изменился...
        int tt = 0;
        private void Pbox_DoubleClick(object? sender, EventArgs e)
        {
            string[] pildid = { "Patrick.png", "Squidward.png", "Spongebob.png" };
            string fail = pildid[tt];
            pbox.Image = Image.FromFile(@"..\..\..\" + fail);
            tt++;
            if (tt == 3) { tt = 0; }
        }

        private void Lbl_MouseHover(object? sender, EventArgs e)
        {
            lbl.ForeColor = Color.Red;
        }

        private void Lbl_MouseLeave(object? sender, EventArgs e)
        {
            lbl.ForeColor = Color.Blue;
        }

        int t = 0;
        private void Btn_Click(object? sender, EventArgs e)
        {
            t++;
            btn.BackColor = (t % 2 == 0) ? Color.Red : Color.Green;
        }

        private void Tree_AfterSelect(object? sender, TreeViewEventArgs e)
        {
            // Убираем элементы, чтобы не дублировать их
            
            Controls.Add(tree); // Обязательно добавляем TreeView обратно на форму

            if (e.Node.Text == "Nupp")
            {
                Controls.Add(btn);
            }
            else if (e.Node.Text == "Silt")
            {
                Controls.Add(lbl);
            }
            else if (e.Node.Text == "Pilt")
            {
                Controls.Add(pbox);
            }
            else if (e.Node.Text == "Markeruut")
            {
                // Создаем и добавляем CheckBox'ы
                chk1 = new CheckBox();
                chk1.Checked = false;
                chk1.Text = e.Node.Text;
                chk1.Size = new Size(100, 30);
                chk1.Location = new Point(150, btn.Height + lbl.Height + pbox.Height + 20);
                chk1.CheckedChanged += new EventHandler(Chk_CheckedChanged);

                chk2 = new CheckBox();
                chk2.Checked = false;
                chk2.Size = new Size(100, 100);
                chk2.Location = new Point(150, btn.Height + lbl.Height + pbox.Height + chk1.Height + 20);
                chk2.BackgroundImage = Image.FromFile(@"..\..\..\Patrick.png");
                chk2.BackgroundImageLayout = ImageLayout.Zoom;
                chk2.CheckedChanged += new EventHandler(Chk_CheckedChanged);

                Controls.Add(chk1);
                Controls.Add(chk2);
            }
            else if (e.Node.Text == "Radionupp") // Проверяем, выбрана ли "Radionupp"
            {
                // Создаем радиокнопки
                int yOffset = 0; // Смещение по Y для размещения радиокнопок
                foreach (var radiobuttonText in rbtn_list)
                {
                    RadioButton rbtn = new RadioButton();
                    rbtn.Text = radiobuttonText;
                    rbtn.Location = new Point(150, btn.Height + lbl.Height + pbox.Height + yOffset);
                    yOffset += 30; // Увеличиваем смещение для следующей радиокнопки
                    rbtn.CheckedChanged += rbtn_CheckedChanged;

                    Controls.Add(rbtn); // Добавляем радиокнопки на форму
                }
            }
            else if (e.Node.Text == "Tekstikast")
            {
                txt = new TextBox();
                txt.Location = new Point(150 + btn.Width + 5, btn.Height);
                txt.Font = new Font("Arial", 28);
                txt.Width = 100;
                txt.TextChanged += Txt_TextChanged;
                Controls.Add(txt);
            }
        }

        private void Txt_TextChanged(object? sender, EventArgs e)
        {
            lbl.Text = txt.Text;
        }

        private void rbtn_CheckedChanged(object? sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            lbl.Text = rb.Text; // Изменяем текст на метке в зависимости от выбранной радиокнопки
        }

        private void Chk_CheckedChanged(object? sender, EventArgs e)
        {
            if (chk1.Checked && chk2.Checked)
            {
                lbl.BorderStyle = BorderStyle.Fixed3D;
                pbox.BorderStyle = BorderStyle.Fixed3D;
            }
            else if (chk1.Checked)
            {
                lbl.BorderStyle = BorderStyle.Fixed3D;
                pbox.BorderStyle = BorderStyle.None;
            }
            else if (chk2.Checked)
            {
                pbox.BorderStyle = BorderStyle.Fixed3D;
                lbl.BorderStyle = BorderStyle.None;
            }
            else
            {
                lbl.BorderStyle = BorderStyle.None;
                pbox.BorderStyle = BorderStyle.None;
            }
        }
    }
}
