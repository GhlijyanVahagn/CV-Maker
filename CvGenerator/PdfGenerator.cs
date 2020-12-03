using CvGenerator.Models;

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvGenerator
{
    public class PdfCvGenerator : ICvGenerator
    {
        public bool Generate(Cv cv )
        {
          
            Document doc = new Document(PageSize.A4, 7f, 5f, 40f, 0f);

            try
            {
                BaseColor black = new BaseColor(0, 0, 0);
                BaseColor blue = new BaseColor(0, 0, 255);
                BaseColor white = new BaseColor(255, 255, 255);


                string newFilePath = Environment.CurrentDirectory + "\\" + $"CV_{cv.PersonalInfo.FullName}.pdf"; 
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(newFilePath, FileMode.Create));
                doc.Open();


                iTextSharp.text.Font CvHeaderFont = FontFactory.GetFont("Segoe UI", 40, blue);
                CvHeaderFont.IsBold();
                CvHeaderFont.IsUnderlined();


                iTextSharp.text.Font mainFont = FontFactory.GetFont("Segoe UI", 22, blue);
                iTextSharp.text.Font infoFont1 = FontFactory.GetFont("Kalinga", 15, black);
                iTextSharp.text.Font expHeadFond = FontFactory.GetFont("Calibri (Body)", 15, black);
                PdfContentByte contentByte = writer.DirectContent;

                ColumnText ct = new ColumnText(contentByte);


                doc.Open();
                PdfPTable modelInfoTable = new PdfPTable(1);
                modelInfoTable.DefaultCell.Border = Rectangle.NO_BORDER;
                modelInfoTable.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell modelInfoCell1 = new PdfPCell();

                modelInfoCell1.PaddingTop = 30f;
                modelInfoCell1.PaddingBottom = 30f;
                modelInfoCell1.Border = 0;
                #region Header

                Phrase mainPharse = new Phrase();
                Chunk infoHeader = new Chunk("CV", CvHeaderFont);
                mainPharse.Add(infoHeader);
                mainPharse.Add(new Chunk(Environment.NewLine));
                #endregion
                #region Personal Info

                Phrase mobPhrase = new Phrase();
                mobPhrase.Add(Environment.NewLine);

                Chunk PersonalInfoheader = new Chunk("PERSONAL INFORMATION", mainFont);
                mobPhrase.Add(PersonalInfoheader);
                mobPhrase.Add(new Chunk(Environment.NewLine));
                mobPhrase.Add(new Chunk(Environment.NewLine));
                
                Chunk mChunk = new Chunk("Full Name - "+cv.PersonalInfo.FullName, infoFont1);
                mobPhrase.Add(mChunk);
                mobPhrase.Add(new Chunk(Environment.NewLine));

                Chunk infoChunk1 = new Chunk("Profile - " + cv.PersonalInfo.Profile, infoFont1);
                mobPhrase.Add(infoChunk1);
                mobPhrase.Add(new Chunk(Environment.NewLine));

                Chunk roleChunk1 = new Chunk("Role - " + cv.PersonalInfo.Role, infoFont1);
                mobPhrase.Add(roleChunk1);
                mobPhrase.Add(new Chunk(Environment.NewLine));
   
                Chunk infoChunk22 = new Chunk("Age - " +cv.PersonalInfo.Age.ToString(), infoFont1);
                mobPhrase.Add(infoChunk22);
                mobPhrase.Add(new Chunk(Environment.NewLine));

                modelInfoTable.AddCell(mainPharse);

                Chunk cmob = new Chunk("Phone - " + cv.PersonalInfo.Phone, infoFont1);
                mobPhrase.Add(cmob);
                mobPhrase.Add(new Chunk(Environment.NewLine));
               
                modelInfoCell1.AddElement(mobPhrase);

                Phrase msgPhrase = new Phrase();
                iTextSharp.text.Font msgFont = FontFactory.GetFont("Kalinga", 10, black);
                Chunk cmsg = new Chunk("EMail - " + cv.PersonalInfo.Email, infoFont1);
   
                msgPhrase.Add(cmsg);
        
                #endregion
                #region Experience
                modelInfoCell1.AddElement(msgPhrase);
                modelInfoTable.AddCell(modelInfoCell1);
            
                PdfPCell cell1 = new PdfPCell()
                {
                    BorderWidthBottom = 0f,
                    BorderWidthTop = 0f,
                    BorderWidthLeft = 0f,
                    BorderWidthRight = 0f
                };
                cell1.PaddingTop = 30f;
                cell1.Border = 0;
                modelInfoTable.AddCell(cell1);
                PdfPCell cellExp = new PdfPCell()
                {
                    BorderWidthBottom = 0f,
                    BorderWidthTop = 0f,
                    BorderWidthLeft = 0f,
                    BorderWidthRight = 0f
                };

                cellExp.Border = 0;
                Phrase ExperiencePhrase = new Phrase();
                Chunk ExperienceChunk = new Chunk("EXPERIENCE", mainFont);
                ExperiencePhrase.Add(ExperienceChunk);
                ExperiencePhrase.Add(new Chunk(Environment.NewLine, mainFont));
                ExperiencePhrase.Add(new Chunk(Environment.NewLine, mainFont));

                cellExp.AddElement(ExperiencePhrase);
                modelInfoTable.AddCell(cellExp);
                
                foreach(var item in cv.Experience)
                {
                  
                    PdfPCell expcell = new PdfPCell()
                    {
                        BorderWidthBottom = 0f,
                        BorderWidthTop = 0f,
                        BorderWidthLeft = 0f,
                        BorderWidthRight = 0f
                    };
                 
                    expcell.PaddingBottom = 10f;

                    Phrase expPhrase = new Phrase();
                    StringBuilder expStringBuilder = new StringBuilder();
                    StringBuilder expStringBuilder1 = new StringBuilder();
               
                    expStringBuilder.Append("Title - "+item.Title + Environment.NewLine);
                    expStringBuilder.Append("Company - " + item.Company + Environment.NewLine);
                  
                    expStringBuilder1.Append("From " + item.From.ToShortDateString() + " To " + item.To.ToShortDateString() + Environment.NewLine);
        
                    Chunk expDetailChunk = new Chunk(expStringBuilder.ToString(), expHeadFond);
                    expPhrase.Add(expDetailChunk);
                    expPhrase.Add(new Chunk(expStringBuilder1.ToString(), infoFont1));
                    expcell.AddElement(expPhrase);
                    modelInfoTable.AddCell(expcell);
                   
                }
                #endregion
                #region Additional
                PdfPCell pCellAdditional = new PdfPCell()
                {
                    BorderWidth = 0f
                };
                pCellAdditional.PaddingTop = 10f;
                pCellAdditional.PaddingBottom = 10f;


                Phrase phMain = new Phrase();

                Chunk expAdditionalInfo = new Chunk("ADDITIONAL INFO", mainFont);
                phMain.Add(new Chunk(Environment.NewLine, mainFont));


                phMain.Add(expAdditionalInfo);
                pCellAdditional.AddElement(phMain);
                modelInfoTable.AddCell(pCellAdditional);
                if (cv.AdditionalInfo.Length > 600)
                {
                    PdfPCell pCell1 = new PdfPCell()
                    {
                        BorderWidth = 0f
                    };
                    PdfPCell pCell2 = new PdfPCell()
                    {
                        BorderWidth = 0f
                    };
                    Phrase ph1 = new Phrase();
                    Phrase ph2 = new Phrase();
                    string experience1 = cv.AdditionalInfo.Substring(0, 599);
                    string experience2 = cv.AdditionalInfo.Substring(599, cv.AdditionalInfo.Length - 600);
                    ph1.Add(new Chunk(experience1, infoFont1));
                    ph2.Add(new Chunk(experience2, infoFont1));
                    pCell1.AddElement(ph1);
                    pCell2.AddElement(ph2);
                    modelInfoTable.AddCell(pCell1);
                    modelInfoTable.AddCell(pCell2);
                }
                else
                {
                    PdfPCell pCell1 = new PdfPCell()
                    {
                        BorderWidth = 0f
                    };
                    Phrase ph1 = new Phrase();
                    string experience1 = cv.AdditionalInfo;
                    ph1.Add(new Chunk(experience1, infoFont1));
                    pCell1.AddElement(ph1);
                    modelInfoTable.AddCell(pCell1);
                }
                #endregion

                doc.Add(modelInfoTable);
                return true;
          
            }
            catch (Exception exception)
            { 
                return false;
                throw exception;
               
            }
            finally
            {
                doc.Close();
            }
        

    }

      
    }
}
