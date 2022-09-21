using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaosGalaxyProfileMaker
{
    internal class ProfileCG
    {
        private Image imageHeroPortrait;
        private Image imageHeroUnit;
        private string filepathHeroPortrait;
        private string filepathHeroUnit;
        private string filepathProfile;
        private string talent;
        private string strategy;
        private string tactics;

        private string story;
        private string conditions;

        private string military;
        private string intellect;
        private string admin;
        private string character;

        private string power;
        private string energy;
        private string agility;
        private string movement;

        private string nameHero;
        private string nameUnit;

        private string type;
        private string[] weapons;
        private string shield;
        private string[] abilities;
        public void LoadPortrait(string filename)
        {
            if (filename == null)
            {
                return;
            }
            if (File.Exists(filename))
            {
                filepathHeroPortrait = filename;
                imageHeroPortrait = Image.FromFile(filename);
            }
        }
        public void LoadUnit(string filename)
        {
            if (filename == null)
            {
                return;
            }
            if (File.Exists(filename))
            {
                filepathHeroUnit = filename;
                imageHeroUnit = Image.FromFile(filename);
            }
        }
        public void SetFilePath(string filename)
        {
            filepathProfile = filename;
        }



        public void ExportProfile(string filename)
        {
            filepathProfile = filename;
            string[] lines =
                {
                "####filepathHeroPortrait",
                filepathHeroPortrait+"",
                "####filepathHeroUnit",
                filepathHeroUnit+"",
                "####talent",
                talent+"",
                "####strategy",
                strategy+"",
                "####tactics",
                tactics+"",
                "####story",
                story+"",
                "####conditions",
                conditions+"",
                "####military",
                military+"",
                "####intellect",
                intellect+"",
                "####admin",
                admin+"",
                "####character",
                character+"",
                "####power",
                power+"",
                "####energy",
                energy+"",
                "####agility",
                agility+"",
                "####movement",
                movement+"",
                "####nameHero",
                nameHero+"",
                "####nameUnit",
                nameUnit+"",
                "####type",
                type+"",
                "####weapons",
                weapons[0]+"",
                weapons[1]+"",
                weapons[2]+"",
                "####shield",
                shield+"",
                "####abilities",
                abilities[0]+"",
                abilities[1]+"",
                abilities[2]+"",
            };


            File.WriteAllLines(filename, lines);
        }


        public void SaveProfile(string filename)
        {
            string fileexported = filename;
            int idx = fileexported.LastIndexOf(".");
            if (idx != -1)
            {
                fileexported = fileexported.Substring(0, idx);
            }
            if (!fileexported.EndsWith(".cgpm"))
            {
                fileexported += ".cgpm";
            }
            if (filepathProfile == null)
            {
                if (!File.Exists(fileexported))
                {
                    ExportProfile(fileexported);
                }
            }
            if (fileexported.Equals(filepathProfile))
            {
                //nothing
            }
            Console.WriteLine(story);

            Font arialFont = new Font("Arial", 10);
            Font arialFontBold = new Font("Arial", 10, FontStyle.Bold);
            int heightTxt = TextRenderer.MeasureText("Name", arialFont).Height;

            int width = 540;
            int heightTop = 200;

            int height = heightTop +20+ (heightTxt+5) * (12+CalculateNbLines(new string[] {conditions,story},width/2-12,heightTxt,arialFont));
            int widthRect = 256;


            Bitmap bitmap = new Bitmap(width, height);




            Color background_color = Color.FromArgb(255, 0xb3, 0xb9, 0xd1);
            Pen penBlack = new Pen(Color.Black);
            penBlack.Width = 2;
            Pen penWhite = new Pen(Color.White);
            penWhite.Width = 1;

            Stream imgStream =Assembly.GetExecutingAssembly().GetManifestResourceStream("ChaosGalaxyProfileMaker.Resources.background.png");
            Image backgroundImg=Image.FromStream(imgStream);

            Rectangle rectsrc, rectdst;
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CompositingMode = CompositingMode.SourceOver;

                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.SmoothingMode = SmoothingMode.None;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;

                graphics.Clear(background_color);
                
                penBlack.Width = 4;
                graphics.DrawLine(penBlack, width / 2, 0, width / 2, height);
                penWhite.Width = 2;
                graphics.DrawLine(penWhite, width / 2, 0, width / 2, height);
                penBlack.Width = 2;

                graphics.DrawRectangle(penBlack, 1, 1, bitmap.Width - 2, bitmap.Height - 2);
                //graphics.FillRectangle(Brushes.White, 6, 6, widthRect, 200);
                //graphics.FillRectangle(Brushes.White, width - widthRect - 6, 6, widthRect, 200);

                graphics.SmoothingMode = SmoothingMode.HighQuality;

         
                
                rectsrc = new Rectangle(0, 0, backgroundImg.Width, backgroundImg.Height);
                rectdst = new Rectangle(6, 6, widthRect, heightTop);
                graphics.DrawImage(backgroundImg, rectdst, rectsrc, GraphicsUnit.Pixel);
                rectdst = new Rectangle(width - widthRect - 6, 6, widthRect, heightTop);
                graphics.DrawImage(backgroundImg, rectdst, rectsrc, GraphicsUnit.Pixel);

                graphics.DrawRectangle(penBlack, 5, 5, widthRect + 2, 202);
                graphics.DrawRectangle(penBlack, width - widthRect - 7, 5, widthRect + 2, 202);


                if (imageHeroPortrait != null)
                {
                    rectsrc = new Rectangle(0, 0, imageHeroPortrait.Width, imageHeroPortrait.Height);
                    rectdst = new Rectangle(6 + (widthRect - 240) / 2, 6, 240, heightTop);
                    graphics.DrawImage(imageHeroPortrait, rectdst, rectsrc, GraphicsUnit.Pixel);
                }
                int startX = 6;
                if (imageHeroUnit != null)
                {
                    rectsrc = new Rectangle(0, 0, imageHeroUnit.Width, imageHeroUnit.Height);
                    rectdst = new Rectangle(width - widthRect - 6, 6 + (heightTop - 128), widthRect, 128);
                    graphics.DrawImage(imageHeroUnit, rectdst, rectsrc, GraphicsUnit.Pixel);
                }

                RectangleF drawRectTxt = new RectangleF(startX, heightTop + 10, width / 2 - 12, 0);

                string[] arrayOfText;
                penBlack.Width = 1;

                //graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;


                drawRectTxt.Height = heightTxt;
                DrawLineMixed("Name: ", nameHero, startX, penBlack, ref drawRectTxt, graphics, arialFont, arialFontBold);

                DrawLineVertical("Story:", startX, ref drawRectTxt, graphics, arialFontBold);
                DrawRectangleOfText(new string[] {story}, heightTxt, penBlack, drawRectTxt, arialFont, graphics);
                DrawLineVertical(story, startX, ref drawRectTxt, graphics, arialFont);
                drawRectTxt.Y += 6;

                DrawLineVertical("Stats: ", startX, ref drawRectTxt, graphics, arialFontBold);
                arrayOfText = new string[] { " - Military: " + military, " - Intellect: " + intellect };
                DrawRectangleOfText(arrayOfText, heightTxt, penBlack, drawRectTxt, arialFont, graphics);
                DrawLineHorizontal(" Military: ", ref drawRectTxt, graphics, arialFontBold);
                float savedX = drawRectTxt.X;
                DrawLineHorizontal(military, ref drawRectTxt, graphics, arialFont);
                drawRectTxt.X = width / 4;
                DrawLineHorizontal("Intellect: ", ref drawRectTxt, graphics, arialFontBold);
                drawRectTxt.X = width / 4+graphics.MeasureString("Character: ", arialFontBold).Width;
                DrawLineVertical(intellect, startX, ref drawRectTxt, graphics, arialFont);

                drawRectTxt.Width = width / 2 - 12;
                DrawLineHorizontal(" Admin: ", ref drawRectTxt, graphics, arialFontBold);
                drawRectTxt.X = savedX;
                DrawLineHorizontal(admin, ref drawRectTxt, graphics, arialFont);
                drawRectTxt.X = width / 4;
                DrawLineHorizontal("Character: ", ref drawRectTxt, graphics, arialFontBold);
                DrawLineVertical(character, startX, ref drawRectTxt, graphics, arialFont);
                drawRectTxt.Width = width / 2 - 12;
                drawRectTxt.Y += 6;


                DrawLineVertical("Skills:", startX, ref drawRectTxt, graphics, arialFontBold);

                arrayOfText = new string[] { "Talent:", talent, "Strategy:", strategy, "Tactics:", tactics };
                DrawRectangleOfText(arrayOfText, heightTxt, penBlack, drawRectTxt, arialFont, graphics);

                DrawLineVertical(arrayOfText[0], startX, ref drawRectTxt, graphics, arialFontBold);
                DrawLineVertical(arrayOfText[1], startX, ref drawRectTxt, graphics, arialFont);
                DrawLineVertical(arrayOfText[2], startX, ref drawRectTxt, graphics, arialFontBold);
                DrawLineVertical(arrayOfText[3], startX, ref drawRectTxt, graphics, arialFont);
                DrawLineVertical(arrayOfText[4], startX, ref drawRectTxt, graphics, arialFontBold);
                DrawLineVertical(arrayOfText[5], startX, ref drawRectTxt, graphics, arialFont);
                drawRectTxt.Y += 6;


                DrawLineVertical("Conditions:", startX, ref drawRectTxt, graphics, arialFontBold);
                DrawRectangleOfText(new string[] { conditions }, heightTxt, penBlack, drawRectTxt, arialFont, graphics);
                DrawLineVertical(conditions, startX, ref drawRectTxt, graphics, arialFont);
                drawRectTxt.Y += 6;

                startX = 6 + width / 2;
                drawRectTxt.X = startX;
                drawRectTxt.Y = heightTop + 10;
                DrawLineMixed("Name: ", nameUnit, startX, penBlack, ref drawRectTxt, graphics, arialFont, arialFontBold);
                DrawLineMixed("Type: ", type, startX, penBlack, ref drawRectTxt, graphics, arialFont, arialFontBold);

                DrawLineVertical("Weapons:", startX, ref drawRectTxt, graphics, arialFontBold);
                DrawRectangleOfText(weapons, heightTxt, penBlack, drawRectTxt, arialFont, graphics);
                for (int i = 0; i < weapons.Length; i++)
                {
                    if (weapons[i]!=null && weapons[i].Trim().Length > 0)
                    {
                        DrawLineVertical(" - " + weapons[i], startX, ref drawRectTxt, graphics, arialFont);
                    }
                    //else
                    //{
                    //    DrawLineVertical(" - ", startX, ref drawRectTxt, graphics, arialFont);
                    //}
                }
                drawRectTxt.Y += 6;
                string shieldtxt;
                if (shield.Length == 0)
                {
                    shieldtxt = "None";
                }
                else
                {
                    shieldtxt = shield;
                }

                DrawLineMixed("Shield: ", shieldtxt, startX,penBlack, ref drawRectTxt, graphics, arialFont, arialFontBold);

                DrawLineVertical("Abilities:", startX, ref drawRectTxt, graphics, arialFontBold);
                DrawRectangleOfText(abilities, heightTxt, penBlack, drawRectTxt, arialFont, graphics);
                for (int i = 0; i < abilities.Length; i++)
                {
                    if (abilities[i] != null && abilities[i].Trim().Length > 0)
                    {
                        DrawLineVertical(" - " + abilities[i], startX, ref drawRectTxt, graphics, arialFont);
                    }
                }
                drawRectTxt.Y += 6;

                arrayOfText = new string[] {  power,  energy, agility,  movement };
                DrawLineVertical("Stats:", startX, ref drawRectTxt, graphics, arialFontBold);

                DrawRectangleOfText(arrayOfText, heightTxt, penBlack, drawRectTxt, arialFont, graphics);


                DrawLineMixed("Power: ", arrayOfText[0], startX, null, ref drawRectTxt, graphics, arialFont, arialFontBold);
                DrawLineMixed("Energy: ", arrayOfText[1], startX,null, ref drawRectTxt, graphics, arialFont, arialFontBold);
                DrawLineMixed("Agility: ", arrayOfText[2], startX,null, ref drawRectTxt, graphics, arialFont, arialFontBold);
                DrawLineMixed("Movement: ", arrayOfText[3], startX,null, ref drawRectTxt, graphics, arialFont, arialFontBold);

            }

            bitmap.Save(filename);//save the image file
        }
        private void DrawRectangleOfText(string[] arrayOfText,float heightTxt,Pen penBlack,RectangleF drawRectTxt,Font arialFont, Graphics graphics)
        {
            int nblines = CalculateNbLines(arrayOfText, drawRectTxt, arialFont, graphics);
            graphics.FillRectangle(Brushes.White, drawRectTxt.X-1 , drawRectTxt.Y, drawRectTxt.Width + 2, heightTxt * nblines + 2);
            graphics.DrawRectangle(penBlack, drawRectTxt.X -1, drawRectTxt.Y, drawRectTxt.Width + 2, heightTxt * nblines + 2);

        }

        private int CalculateNbLines(string[] texts, RectangleF drawRectTxt, Font arialFont, Graphics graphics)
        {
            int nbLines=0;
            for (int i = 0; i < texts.Length; i++)
            {
                SizeF size2 = graphics.MeasureString(texts[i], arialFont);

                nbLines += (int)(size2.Height / drawRectTxt.Height);
                nbLines += (int)(size2.Width / drawRectTxt.Width);
            }
            return nbLines;
        }
        private int CalculateNbLines(string[] texts, float widthRect, float heightRect, Font arialFont)
        {
            int nbLines = 0;
            for (int i = 0; i < texts.Length; i++)
            {
                Console.WriteLine(texts[i]);

                SizeF size2 = TextRenderer.MeasureText(texts[i], arialFont);
                Console.WriteLine(size2 + "     " + widthRect + ":" + heightRect);

                nbLines += (int)(size2.Height / heightRect);
                nbLines += (int)(size2.Width / widthRect);
                Console.WriteLine(nbLines);

            }
            return nbLines;
        }

        private void DrawLineMixed(string text1, string text2, int xStart,Pen penBlack, ref RectangleF drawRectTxt, Graphics graphics, Font arialFont, Font arialFontbold)
        {
            float savedHeight = drawRectTxt.Height;
            SizeF size = graphics.MeasureString(text1, arialFontbold, (int)(drawRectTxt.Width / 2 - 12));

            drawRectTxt.Height = size.Height;
            graphics.DrawString(text1, arialFontbold, Brushes.Black, drawRectTxt);
            drawRectTxt.X += size.Width;
            drawRectTxt.Width -=size.Width;

            SizeF size2 = graphics.MeasureString(text2, arialFont);
            drawRectTxt.Height = savedHeight;

            int nblines = (int)(size2.Height / drawRectTxt.Height);
            nblines += (int)(size2.Width / drawRectTxt.Width);



            drawRectTxt.Height = nblines*size.Height;

            if (penBlack != null)
            {
                DrawRectangleOfText(new string[] { text2 }, size.Height, penBlack, drawRectTxt, arialFont, graphics);
            }

            graphics.DrawString(text2, arialFont, Brushes.Black, drawRectTxt);
            drawRectTxt.Y += drawRectTxt.Height;
            drawRectTxt.X = xStart;
            drawRectTxt.Width += size.Width;
            drawRectTxt.Height = savedHeight;
            if (penBlack != null)
            {
                drawRectTxt.Y += 6;
            }
        }
        private void DrawLineHorizontal(string text, ref RectangleF drawRectTxt, Graphics graphics, Font arialFont)
        {

            SizeF size = graphics.MeasureString(text, arialFont);
            graphics.DrawString(text, arialFont, Brushes.Black, drawRectTxt);
            drawRectTxt.X += size.Width;
            drawRectTxt.Width-= size.Width;
        }

        private void DrawLineVertical(string text, int xStart,ref RectangleF drawRectTxt, Graphics graphics, Font arialFont )
        {

            float savedHeight = drawRectTxt.Height;

            SizeF size2 = graphics.MeasureString(text, arialFont);

            int nblines = (int)(size2.Height / drawRectTxt.Height);
            //while(size2.Width> drawRectTxt.Width/4)
            //{
            //    size2.Width -= drawRectTxt.Width;
            //    nblines += 1;
            //}
            nblines += (int)(size2.Width / drawRectTxt.Width);

            drawRectTxt.Height = nblines* savedHeight;

            graphics.DrawString(text, arialFont, Brushes.Black, drawRectTxt);
            drawRectTxt.Y += drawRectTxt.Height;
            drawRectTxt.X = xStart;
            drawRectTxt.Height = savedHeight;
        }

        public string GetFileName()
        {
            return filepathProfile;
        }
        public void SetStartingStats(string military, string intellect, string admin, string character)
        {
            this.military = military;
            this.intellect = intellect;
            this.admin = admin;
            this.character = character;
        }

        public void SetUnitStats(string power, string energy, string agility, string movement)
        {
            this.power = power;
            this.energy = energy;
            this.agility = agility;
            this.movement = movement;

        }
        public void SetUnitEquipment(string type, string[] weapons, string shield, string[] abilities)
        {
            this.type = type;
            this.weapons = weapons;
            this.shield = shield;
            this.abilities = abilities;
        }


        public void SetNames(string nameHero, string nameUnit)
        {
            this.nameHero = nameHero;
            this.nameUnit = nameUnit;
        }
        public void SetStoryAndConditions(string story, string conditions)
        {
            this.story = story;
            this.conditions = conditions;
        }

        public void SetSkills(string talent, string strategy, string tactics)
        {
            this.talent = talent;
            this.strategy = strategy;
            this.tactics = tactics;
        }
    }
}
