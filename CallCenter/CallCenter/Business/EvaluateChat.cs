using CallCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallCenter.Business
{
    public class EvaluateChat
    {
        public ResponseModel CalculatePoints(string message, string title)
        {
            ResponseModel rm = new ResponseModel();
            int totalPoints = 0;
            List<string> chatList = message.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            if (chatList.Count() <= 1)
            {
                rm.isCheck = false;
                rm.title = title;
                rm.points = totalPoints;
                return rm;
            }
            else
            {
                totalPoints += CalculateLines(chatList);
                totalPoints += SearchWord(chatList);
                totalPoints += CalculateHours(chatList);
                rm.isCheck = true;
                rm.title = title;
                rm.points = totalPoints;
                return rm;
            }
        }
        private int CalculateLines(List<string> chatList)
        {
            int lines = 0;
            int points = 0;

            foreach (var item in chatList)
            {
                lines++;
            }

            if (lines < 1)
            {
                points += -100;
            }
            else if (lines < 5 && lines > 1)
            {
                points += 20;
            }
            else
            {
                points += 10;
            }

            return points;
        }

        private int SearchWord(List<string> chatList)
        {
            int points = 0;

            foreach (var item in chatList)
            {
                if (item.Contains("URGENTE"))
                {
                    points += -10;
                }
                else if (item.Contains("EXCELENTE SERVICIO"))
                {
                    points += 100;
                }
            }
            return points;
        }

        private int CalculateHours(List<string> chatList)
        {
            int points = 0;
            DateTime startHour = Convert.ToDateTime(chatList[0].Substring(0, 8));
            DateTime endHour = Convert.ToDateTime(chatList.Last().Substring(0, 8));

            TimeSpan newDate = endHour - startHour;

            if (newDate.Minutes < 60)
            {
                points = 50;
            }
            else
            {
                points = 25;
            }

            return points;
        }
    }
}
