using System;
using System.Collections.Generic;

namespace VKAPI.Model
{
    public class Message
    {
        //идентификатор сообщения (не возвращается для пересланных сообщений).
        public string Id { get; set; }
        //дата отправки сообщения в формате unixtime.
        public DateTime Date { get; set; }
        //тип сообщения (0 — полученное, 1 — отправленное, не возвращается для пересланных сообщений).
        public double Out { get; set; }
        //идентификатор пользователя, в диалоге с которым находится сообщение.
        public int UserId { get; set; }
        //статус сообщения (0 — не прочитано, 1 — прочитано, не возвращается для пересланных сообщений).
        public int ReadState { get; set; }
        //заголовок сообщения или беседы.
        public string Title { get; set; }
        //текст сообщения.
        public string Body { get; set; }
        //идентификатор беседы
        public int ChatId { get; set; }
        //идентификаторы авторов последних сообщений беседы.
        public List<int> ChatActive { get; set; }
        //количество участников беседы.
        public int UsersCount { get; set; }
        //идентификатор создателя беседы.
        public int AdminId { get; set; }

          //     <message>
                    // <id>42273</id>
                    // <date>1428815619</date>
                    // <out>0</out>
                    // <user_id>7954808</user_id>
                    // <read_state>1</read_state>
                    // <title>Миша, Алексей, Александр</title>
                    // <body>Ахахахха</body>
                    // <chat_id>114</chat_id>
                        // <chat_active list="true"> 
                            //  <item>89415824</item>
                            //  <item>7954808</item>
                            //  <item>159619107</item>
                        // </chat_active>
                        // <push_settings>
                            //  <sound>1</sound>
                            //  <disabled_until>18</disabled_until>
                        // </push_settings>
                    // <users_count>4</users_count>
                    // <admin_id>89415824</admin_id>
                //</message>
    }
}