namespace MinecraftRPG
{
    partial class CraftingTable
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbRecipes = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rtbRecipeDetails = new System.Windows.Forms.RichTextBox();
            this.btnConfirmCraft = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbRecipes
            // 
            this.lbRecipes.FormattingEnabled = true;
            this.lbRecipes.ItemHeight = 20;
            this.lbRecipes.Location = new System.Drawing.Point(12, 42);
            this.lbRecipes.Name = "lbRecipes";
            this.lbRecipes.Size = new System.Drawing.Size(295, 344);
            this.lbRecipes.TabIndex = 0;
            this.lbRecipes.SelectedIndexChanged += new System.EventHandler(this.lbRecipes_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Recipes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(337, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Recipe Details";
            // 
            // rtbRecipeDetails
            // 
            this.rtbRecipeDetails.Location = new System.Drawing.Point(341, 42);
            this.rtbRecipeDetails.Name = "rtbRecipeDetails";
            this.rtbRecipeDetails.ReadOnly = true;
            this.rtbRecipeDetails.Size = new System.Drawing.Size(358, 284);
            this.rtbRecipeDetails.TabIndex = 3;
            this.rtbRecipeDetails.Text = "";
            // 
            // btnConfirmCraft
            // 
            this.btnConfirmCraft.Enabled = false;
            this.btnConfirmCraft.Location = new System.Drawing.Point(442, 340);
            this.btnConfirmCraft.Name = "btnConfirmCraft";
            this.btnConfirmCraft.Size = new System.Drawing.Size(160, 35);
            this.btnConfirmCraft.TabIndex = 4;
            this.btnConfirmCraft.Text = "Missing Materials";
            this.btnConfirmCraft.UseVisualStyleBackColor = true;
            this.btnConfirmCraft.Click += new System.EventHandler(this.btnCraft_Click);
            // 
            // CraftingTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 401);
            this.Controls.Add(this.btnConfirmCraft);
            this.Controls.Add(this.rtbRecipeDetails);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbRecipes);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CraftingTable";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Crafting Table";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbRecipes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rtbRecipeDetails;
        private System.Windows.Forms.Button btnConfirmCraft;
    }
}