DROP DATABASE IF EXISTS MovieEngagement;

CREATE DATABASE MovieEngagement;
USE MovieEngagement;

CREATE TABLE IF NOT EXISTS Customer(
		customer_id int(11) unique auto_increment not null,
        name nvarchar(30) not null,
        customer_email nvarchar(50) not null,
        customer_phone nvarchar(15) not null,
        user_name nvarchar(50) not null,
        password varchar(50) not null,
        account_bank varchar(20) not null,
        birthday date not null,
        address nvarchar(200) not null,
        sex nvarchar(3) not null,
        register_date datetime not null default current_timestamp()
);



Select * from Customer where customer_id = 1;
-- Select count(*) as NumberOfCustomer From Customer  where customer_email = 'valentinolivgr@gmail.com' and password = '123456';   
-- update Customer set user_name = 'Dat hihi', password = '1234',name = 'Tran Manh Dat ' , customer_email = 'valen@gmail.com',customer_phone = '0988968289',address = 'Hung Yen - VN' 
-- where customer_id = 1;
CREATE TABLE IF NOT EXISTS Movies(
		movie_id int(11) unique auto_increment not null,
        movie_name nvarchar(50) unique not null,
        actor nvarchar(255) not null,
        producers nvarchar(160) not null,
        director nvarchar(80) not null,
        genre nvarchar(50) not null,
        duration int(5) not null,
        detail_movie nvarchar(5000) not null,
        release_date date not null,
        end_date date not null
);


CREATE TABLE IF NOT EXISTS Rooms(
		room_id int(11) unique auto_increment not null,
        name varchar(50) not null unique,
        number_of_seats int(100) not null,
        map_seat nvarchar(2000) not null,
        map_VIP nvarchar(500),
        chaire_not_placed nvarchar(200) 
);


CREATE TABLE IF NOT EXISTS Schedules(
		schedule_id int(11) unique auto_increment not null,
        movie_id int(11) not null,
        room_id int(11) not null,
        show_date date not null,
        start_time time not null,
        end_time time not null,
        price decimal(10,2) not null,
        schedule_room_seat nvarchar(2000) not null,
        
        FOREIGN KEY (movie_id) REFERENCES Movies(movie_id) ON DELETE CASCADE,
        FOREIGN KEY (room_id) REFERENCES Rooms(room_id) ON DELETE CASCADE
);

CREATE UNIQUE INDEX IDX_SCHEDULE ON Schedules(room_id,show_date,start_time,end_time);

CREATE TABLE IF NOT EXISTS PriceSeat(
		price_id int(11) unique auto_increment not null,
        ticketType nvarchar(10) not null,
        price decimal(10,2) not null 
);
Select * from PriceSeat;

CREATE TABLE IF NOT EXISTS Reservation(
		reservation_id int(11) unique auto_increment not null,
        customer_Id int(11) not null,
        schedule_id int(11) not null,
        no_of_seat varchar(30) not null,
        code_ticket int not null,
        price decimal(10,2) not null,
        create_on datetime not null default current_timestamp(),
        FOREIGN KEY (customer_Id) REFERENCES Customer(customer_id) ON DELETE CASCADE,
        FOREIGN KEY (schedule_id) REFERENCES Schedules(schedule_id) ON DELETE CASCADE
);
sELECT * FROM Reservation;

INSERT INTO PriceSeat(ticketType,price) VALUE 
('Ghế thường','45000.00'),
('Ghế Vip','60000.00');

-- Insert data for the table Customer

INSERT INTO Customer (name,customer_email,customer_phone,user_name,password,birthday,address,sex,account_bank) VALUE 
('Trần Mạnh Đạt', 'valentinolivgr@gmail.com','0988968289','Valentino','123456','1997-11-20','Hưng Yên, Việt Nam','Nam','09898928181822');
select * from Customer where user_name = 'Đạt liv';
Select * From Customer  where user_name = 'Đạt liv' and password = '123456';

INSERT INTO Customer (name,customer_email,customer_phone,user_name,password,birthday,address,sex,account_bank) VALUE 
('Harry Potter', 'harrypottet@gmail.com','0988968289','USERNAME','123456','1997-11-20','Hưng Yên, Việt Nam','Nam','09898928181822');
-- Insert data for the table Movies

INSERT INTO Movies(movie_name,actor,producers,director,genre,duration,detail_movie,release_date,end_date) VALUE 
('HARRY POTTER','Harry Potter - Hermione Granger - Ron Weasley ...','Warner Bros., Heyday Films, 1492 Pictures','Chris Columbus',N'Phim viễn tưởng,Phim phiêu lưu','118',
N'Những cuộc phiêu lưu tập trung vào cuộc chiến của Harry Potter trong việc chống lại tên Chúa tể hắc ám Voldemort - người có tham vọng muốn trở nên bất tử, thống trị thế giới phù thủy,  nô dịch hóa những người phi pháp thuật và tiêu diệt những ai cản đường hắn đặc biệt là Harry Potter.','2018-07-20','2018-09-10');

INSERT INTO Movies(movie_name,actor,producers,director,genre,duration,detail_movie,release_date,end_date) VALUE 
(N'GIA ĐÌNH SIÊU NHÂN','Samuel L. Jackson, Sophia Bush, Holly Hunter','Pixar Animation Studios, Walt Disney Pictures','Brad Bird',N'Phim hoạt hình','132',
N'Gia Đình Siêu Nhân đánh dấu những chuyến phiêu lưu anh hùng    đầy hấp dẫn với sự đổi vai đầy táo bạo. Lần này,mẹ dẻo Helen (Elastigirl) sẽ đi thực chiến giải cứu thế giới trong khi bố khỏe Bob (Mr. Incredible) lùi về hậu phương trông nom những  đứa con siêu nhân láu lỉnh. ','2018-07-15','2018-09-05');

INSERT INTO Movies(movie_name,actor,producers,director,genre,duration,detail_movie,release_date,end_date) VALUE 
('THẾ GIỚI KHỦNG LONG','Bryce Dallas Howard, Chris Pratt, Jeff Goldblum,..','Amblin Entertainment, Apaches Entertainment','J.A. Bayona','Phim hành động, phiêu lưu, giả tưởng','128',
'Bốn năm sau thảm họa Công Viên kỷ Jura bị hủy diệt. Một vài    con khủng  long vẫn còn sống sót trong khi hòn đảo Isla      Nublar bị con người bỏ hoang.Owen và Claire quyết định tiến  hành chiến dịch giải cứu những con khủng long còn sống sót   khỏi sự tuyệt chủng khi ngọn núi lửa tại khu vực này bắt đầu hoạt động trở lại. Họ vô tình khám phá ra một âm mưu có thể  khiến toàn bộ hành tinh đối mặt với một hiểm họa to lớn chưa từng thấy kể từ thời tiền sử.','2018-07-20','2018-09-10');

INSERT INTO Movies(movie_name,actor,producers,director,genre,duration,detail_movie,release_date,end_date) VALUE 
('BIỆT ĐỘI CÚN CƯNG','Alan Cumming, Natasha Lyonne, Will Arnett','Alan Cumming, Natasha Lyonne, Will Arnett','Raja Gosnell','Hài Hước, Phiêu Lưu, Gia Đình','92',
'Đặc vụ FBI Frank bất đắc dĩ phải trở thành cộng sự với chú chó Max để cùng triệt phá đường dây buôn lậu động vật. Cùng với  sự giúp sức của biệt đội cún cưng, họ đã cùng nhau trải qua  những tình huống dở khóc dở cười.  Liệu bọn họ có hoàn thành nhiệm vụ một cách thành công? Hãy cùng theo dõi hành trình   phá án của Frank và các chú chó này nhé.','2018-07-20','2018-09-15');

INSERT INTO Movies(movie_name,actor,producers,director,genre,duration,detail_movie,release_date,end_date) VALUE 
('EM GÁI MƯA','Mai Tài Phến, Lê Thùy Linh, Phương Anh Đào...','LOOTES','Kawaii Tuấn Anh','Học đường, tâm lý, lãng mạn','102',
'Em Gái Mưa lấy bối cảnh những năm đầu 2000 về Nguyễn Hà Vy     (Thùy Linh), cô nữ sinh và nhóm bạn thân lớp 12A1 trường     trung học Thanh Xuân gồm lớp trưởng đẹp trai Tuấn Nam, cặp   đôi oan gia Khánh Chi (Trang Hí), Mạnh Hiếu,anh chàng chuyên gia “đâm bể xuồng" Tuấn Nam... Hà Vy tưởng chừng như sẽ “kết thúc thời học sinh trong nhạt nhẽo”mà không có lấy cho riêng mình một kỷ niệm sâu sắc nào thì bỗng gặp được thầy giáo     Minh Vũ (Mai Tài Phến) điển trai có nụ cười tỏa nắng trong   đoàn giáo sinh thực tập.',
'2018-07-18','2018-09-18');


INSERT INTO Movies(movie_name,actor,producers,director,genre,duration,detail_movie,release_date,end_date) VALUE 
('GIÀNH ANH TỪ BIỂN','Shailene Woodley, Sam Claflin, Jeffrey Thomas...','Elizabeth Hawthorne','Baltasar Kormákur','Tâm Lý, Tình cảm','120',
'Tới Tahiti để theo đuổi những chuyến phiêu lưu, Tami Oldham    nhanh chóng phải lòng chàng thủy thủ Richard Sharp và cả hai thực hiện chuyến du lịch mơ ước xuyên Thái Bình Dương. Nhưng cả hai không ngờ rằng họ sẽ đi thẳng vào một cơn bão thảm    khốc nhất trong lịch sử. Sau cơn bão, Richard bị thương nặng còncon thuyền đã bị phá tan. Không có cơ hội được cứu, Tami  phải tìm lấy sức mạnh và lòng quyết tâm để cứu lấy bản thân  và người đàn ông duy nhất cô từng yêu. ',
'2018-07-12','2018-09-12');


INSERT INTO Movies(movie_name,actor,producers,director,genre,duration,detail_movie,release_date,end_date) VALUE 
('GIẢI CỨU CÔNG CHÚA','Nadiya Dorofeeva, Oleksiy Zavhorodn','Chernomor','Oleh Malamuzh','Hoạt hình','120',
'Cứ mỗi trăm năm, lão phù thủy độc ác Chernomor lại bắt cóc một nàng công chúa, nhưng lần này nàng công chúa Mila chẳng phải dạng vừa. Hơn nữa, nàng còn có người yêu là hiệp sĩ Ruslan   dũng cảm, mèo bự biến hình, chuột hamster thông minh, chim   hoàng yến gan dạ ... Nào cùng lên đường GIẢI CỨU CÔNG CHÚA.',
'2018-07-18','2018-09-18');

INSERT INTO Movies(movie_name,actor,producers,director,genre,duration,detail_movie,release_date,end_date) VALUE 
('BẪY THỜI GIAN','Cassidy Erin Gifford, Reiley McClendon','ABCD','Mark Dennis','Khoa học viễn tưởng','83',
'Bẫy Thời Gian - Time Trap phim kể về một giáo sư khảo cổ gặp   nạn khi đang đi tìm tung tích bố mẹ và em gái. Một nhóm sinh viên quyết định đi tìm thầy và họ rơi vào hang động kỳ quái. Câu chuyện giờ đây mới bắt đầu với nhiều bí ẩn và kịch tính  đan xen.',
'2018-07-20','2018-09-10');

-- Insert data for the table Rooms

INSERT INTO Rooms(name,number_of_seats,map_seat,map_VIP,chaire_not_placed) VALUE 
('P1','140','A B C D E F G H I J K L M N;10;','F1,F2,F3',''),
('P2','120','A B C D E F G H I J K L;10;','F1,F2,F3','A9,A10,L3,L4,L5,L6,L7,L8,L9,L10'),
('P3','140','A B C D E F G H I J K L M N;10;','F1,F2,F3','K1,K2,L1,L2,K9,K10,L9,L10,M9,M10,N9,N10'),
('P4','140','A B C D E F G H I J K L M N;10;','F1,F2,F3','D9,D10,E9,E10,F9,F10,G9,G10,H9,H10,I9,I10,J9,J10,M9,M10,N9,N10'),
('P5','100','A B C D E F G H I J;10;','F1,F2,F3','A9,A10,J9,J10'),
('P6','140','A B C D E F G H I J K L M N;10;','F1,F2,F3','A9,A10,B9,B10,M9,M10,N9,N10'),
('P7','140','A B C D E F G H I J K L M N;10;','F1,F2,F3','N3,N4,N5,N6,N7,N8,N9,N10,M3,M4,M5,M6,M7,M8,M9,M10'),
('P8','140','A B C D E F G H I J K L M N;10;','F1,F2,F3','');

Select * from Movies;
-- Insert data for the table Schedules date 20 - 21 - 22 day to room A1 A2 A3 A4 A5 A6 A7 A8
INSERT INTO Schedules(movie_id,room_id,show_date,start_time,end_time,price,schedule_room_seat) VALUE
('1','1','2018-07-31','10:15:00','12:15:00','45000','A B C D E F G H I J K L M N;10;'),
('1','2','2018-07-31','10:30:00','12:30:00','45000','A B C D E F G H I J K L;10;'),
('1','1','2018-07-31','13:45:00','15:45:00','45000','A B C D E F G H I J K L M N;10;'),
('1','2','2018-07-31','19:00:00','21:00:00','45000','A B C D E F G H I J K L;10;'),
('1','1','2018-07-31','19:15:00','21:15:00','45000','A B C D E F G H I J K L M N;10;'),
('1','1','2018-07-31','21:30:00','23:30:00','45000','A B C D E F G H I J K L M N;10;'),
('1','1','2018-08-01','10:15:00','12:15:00','45000','A B C D E F G H I J K L M N;10;'),
('1','2','2018-08-01','10:30:00','12:30:00','45000','A B C D E F G H I J K L;10;'),
('1','1','2018-08-01','13:45:00','15:45:00','45000','A B C D E F G H I J K L M N;10;'),
('1','2','2018-08-01','19:00:00','21:00:00','45000','A B C D E F G H I J K L;10;'),
('1','1','2018-08-01','19:15:00','21:15:00','45000','A B C D E F G H I J K L M N;10;'),
('1','1','2018-08-01','21:30:00','23:30:00','45000','A B C D E F G H I J K L M N;10;');
INSERT INTO Schedules(movie_id,room_id,show_date,start_time,end_time,price,schedule_room_seat) VALUE
('1','1','2018-08-02','10:15:00','12:15:00','45000','A B C D E F G H I J K L M N;10;'),
('1','2','2018-08-02','10:30:00','12:30:00','45000','A B C D E F G H I J K L;10;'),
('1','1','2018-08-02','13:45:00','15:45:00','45000','A B C D E F G H I J K L M N;10;'),
('1','2','2018-08-02','19:00:00','21:00:00','45000','A B C D E F G H I J K L;10;'),
('1','1','2018-08-02','19:15:00','21:15:00','45000','A B C D E F G H I J K L M N;10;'),
('1','1','2018-08-02','21:30:00','23:30:00','45000','A B C D E F G H I J K L M N;10;'),
('1','1','2018-08-03','10:15:00','12:15:00','45000','A B C D E F G H I J K L M N;10;'),
('1','2','2018-08-03','10:30:00','12:30:00','45000','A B C D E F G H I J K L;10;'),
('1','1','2018-08-03','13:45:00','15:45:00','45000','A B C D E F G H I J K L M N;10;'),
('1','2','2018-08-03','19:00:00','21:00:00','45000','A B C D E F G H I J K L;10;'),
('1','1','2018-08-03','19:15:00','21:15:00','45000','A B C D E F G H I J K L M N;10;'),
('1','1','2018-08-03','21:30:00','23:30:00','45000','A B C D E F G H I J K L M N;10;'),
('1','1','2018-08-04','10:15:00','12:15:00','45000','A B C D E F G H I J K L M N;10;'),
('1','2','2018-08-04','10:30:00','12:30:00','45000','A B C D E F G H I J K L;10;'),
('1','1','2018-08-04','13:45:00','15:45:00','45000','A B C D E F G H I J K L M N;10;'),
('1','2','2018-08-04','19:00:00','21:00:00','45000','A B C D E F G H I J K L;10;'),
('1','1','2018-08-04','19:15:00','21:15:00','45000','A B C D E F G H I J K L M N;10;'),
('1','1','2018-08-04','21:30:00','23:30:00','45000','A B C D E F G H I J K L M N;10;'),
('1','1','2018-08-05','10:15:00','12:15:00','45000','A B C D E F G H I J K L M N;10;'),
('1','2','2018-08-05','10:30:00','12:30:00','45000','A B C D E F G H I J K L;10;'),
('1','1','2018-08-05','13:45:00','15:45:00','45000','A B C D E F G H I J K L M N;10;'),
('1','2','2018-08-05','19:00:00','21:00:00','45000','A B C D E F G H I J K L;10;'),
('1','1','2018-08-05','19:15:00','21:15:00','45000','A B C D E F G H I J K L M N;10;'),
('1','1','2018-08-05','21:30:00','23:30:00','45000','A B C D E F G H I J K L M N;10;'),
('1','1','2018-08-06','10:15:00','12:15:00','45000','A B C D E F G H I J K L M N;10;'),
('1','2','2018-08-06','10:30:00','12:30:00','45000','A B C D E F G H I J K L;10;'),
('1','1','2018-08-06','13:45:00','15:45:00','45000','A B C D E F G H I J K L M N;10;'),
('1','2','2018-08-06','19:00:00','21:00:00','45000','A B C D E F G H I J K L;10;'),
('1','1','2018-08-06','19:15:00','21:15:00','45000','A B C D E F G H I J K L M N;10;'),
('1','1','2018-08-06','21:30:00','23:30:00','45000','A B C D E F G H I J K L M N;10;'),
('1','1','2018-08-07','10:15:00','12:15:00','45000','A B C D E F G H I J K L M N;10;'),
('1','2','2018-08-07','10:30:00','12:30:00','45000','A B C D E F G H I J K L;10;'),
('1','1','2018-08-07','13:45:00','15:45:00','45000','A B C D E F G H I J K L M N;10;'),
('1','2','2018-08-07','19:00:00','21:00:00','45000','A B C D E F G H I J K L;10;'),
('1','1','2018-08-07','19:15:00','21:15:00','45000','A B C D E F G H I J K L M N;10;'),
('1','1','2018-08-07','21:30:00','23:30:00','45000','A B C D E F G H I J K L M N;10;'),
('1','1','2018-08-08','10:15:00','12:15:00','45000','A B C D E F G H I J K L M N;10;'),
('1','2','2018-08-08','10:30:00','12:30:00','45000','A B C D E F G H I J K L;10;'),
('1','1','2018-08-08','13:45:00','15:45:00','45000','A B C D E F G H I J K L M N;10;'),
('1','2','2018-08-08','19:00:00','21:00:00','45000','A B C D E F G H I J K L;10;'),
('1','1','2018-08-08','19:15:00','21:15:00','45000','A B C D E F G H I J K L M N;10;'),
('1','1','2018-08-08','21:30:00','23:30:00','45000','A B C D E F G H I J K L M N;10;');

INSERT INTO Schedules(movie_id,room_id,show_date,start_time,end_time,price,schedule_room_seat) VALUE
('2','1','2018-08-02','08:00:00','10:15:00','45000','A B C D E F G H I J K L M N;10;'),
('2','2','2018-08-02','08:15:00','10:30:00','45000','A B C D E F G H I J K L;10;'),
('2','2','2018-08-02','11:00:00','13:15:00','45000','A B C D E F G H I J K L;10;'),
('2','2','2018-08-02','14:00:00','16:15:00','45000','A B C D E F G H I J K L;10;'),
('2','1','2018-08-02','16:00:00','18:15:00','45000','A B C D E F G H I J K L M N;10;'),
('2','2','2018-08-02','16:30:00','18:45:00','45000','A B C D E F G H I J K L;10;'),
('2','2','2018-08-02','21:15:00','23:30:00','45000','A B C D E F G H I J K L;10;'),
('2','1','2018-08-03','08:00:00','10:15:00','45000','A B C D E F G H I J K L M N;10;'),
('2','2','2018-08-03','08:15:00','10:30:00','45000','A B C D E F G H I J K L;10;'),
('2','2','2018-08-03','11:00:00','13:15:00','45000','A B C D E F G H I J K L;10;'),
('2','2','2018-08-03','14:00:00','16:15:00','45000','A B C D E F G H I J K L;10;'),
('2','1','2018-08-03','16:00:00','18:15:00','45000','A B C D E F G H I J K L M N;10;'),
('2','2','2018-08-03','16:30:00','18:45:00','45000','A B C D E F G H I J K L;10;'),
('2','2','2018-08-03','21:15:00','23:30:00','45000','A B C D E F G H I J K L;10;'),
('2','1','2018-08-04','08:00:00','10:15:00','45000','A B C D E F G H I J K L M N;10;'),
('2','2','2018-08-04','08:15:00','10:30:00','45000','A B C D E F G H I J K L;10;'),
('2','2','2018-08-04','11:00:00','13:15:00','45000','A B C D E F G H I J K L;10;'),
('2','2','2018-08-04','14:00:00','16:15:00','45000','A B C D E F G H I J K L;10;'),
('2','1','2018-08-04','16:00:00','18:15:00','45000','A B C D E F G H I J K L M N;10;'),
('2','2','2018-08-04','16:30:00','18:45:00','45000','A B C D E F G H I J K L;10;'),
('2','2','2018-08-04','21:15:00','23:30:00','45000','A B C D E F G H I J K L;10;'),
('2','1','2018-08-05','08:00:00','10:15:00','45000','A B C D E F G H I J K L M N;10;'),
('2','2','2018-08-05','08:15:00','10:30:00','45000','A B C D E F G H I J K L;10;'),
('2','2','2018-08-05','11:00:00','13:15:00','45000','A B C D E F G H I J K L;10;'),
('2','2','2018-08-05','14:00:00','16:15:00','45000','A B C D E F G H I J K L;10;'),
('2','1','2018-08-05','16:00:00','18:15:00','45000','A B C D E F G H I J K L M N;10;'),
('2','2','2018-08-05','16:30:00','18:45:00','45000','A B C D E F G H I J K L;10;'),
('2','2','2018-08-05','21:15:00','23:30:00','45000','A B C D E F G H I J K L;10;'),
('2','1','2018-08-06','08:00:00','10:15:00','45000','A B C D E F G H I J K L M N;10;'),
('2','2','2018-08-06','08:15:00','10:30:00','45000','A B C D E F G H I J K L;10;'),
('2','2','2018-08-06','11:00:00','13:15:00','45000','A B C D E F G H I J K L;10;'),
('2','2','2018-08-06','14:00:00','16:15:00','45000','A B C D E F G H I J K L;10;'),
('2','1','2018-08-06','16:00:00','18:15:00','45000','A B C D E F G H I J K L M N;10;'),
('2','2','2018-08-06','16:30:00','18:45:00','45000','A B C D E F G H I J K L;10;'),
('2','2','2018-08-06','21:15:00','23:30:00','45000','A B C D E F G H I J K L;10;'),
('2','1','2018-08-07','08:00:00','10:15:00','45000','A B C D E F G H I J K L M N;10;'),
('2','2','2018-08-07','08:15:00','10:30:00','45000','A B C D E F G H I J K L;10;'),
('2','2','2018-08-07','11:00:00','13:15:00','45000','A B C D E F G H I J K L;10;'),
('2','2','2018-08-07','14:00:00','16:15:00','45000','A B C D E F G H I J K L;10;'),
('2','1','2018-08-07','16:00:00','18:15:00','45000','A B C D E F G H I J K L M N;10;'),
('2','2','2018-08-07','16:30:00','18:45:00','45000','A B C D E F G H I J K L;10;'),
('2','2','2018-08-07','21:15:00','23:30:00','45000','A B C D E F G H I J K L;10;'),
('2','2','2018-08-08','08:15:00','10:30:00','45000','A B C D E F G H I J K L;10;'),
('2','2','2018-08-08','11:00:00','13:15:00','45000','A B C D E F G H I J K L;10;'),
('2','2','2018-08-08','14:00:00','16:15:00','45000','A B C D E F G H I J K L;10;'),
('2','1','2018-08-08','16:00:00','18:15:00','45000','A B C D E F G H I J K L M N;10;'),
('2','2','2018-08-08','16:30:00','18:45:00','45000','A B C D E F G H I J K L;10;'),
('2','2','2018-08-08','21:15:00','23:30:00','45000','A B C D E F G H I J K L;10;');


INSERT INTO Schedules(movie_id,room_id,show_date,start_time,end_time,price,schedule_room_seat) VALUE
('3','3','2018-08-02','08:00:00','10:10:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-02','10:40:00','12:50:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-02','13:30:00','15:40:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-02','16:00:00','18:10:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-02','18:30:00','20:40:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-02','21:15:00','23:25:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-03','08:00:00','10:10:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-03','10:40:00','12:50:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-03','13:30:00','15:40:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-03','16:00:00','18:10:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-03','18:30:00','20:40:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-03','21:15:00','23:25:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-04','08:00:00','10:10:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-04','10:40:00','12:50:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-04','13:30:00','15:40:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-04','16:00:00','18:10:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-04','18:30:00','20:40:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-04','21:15:00','23:25:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-05','08:00:00','10:10:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-05','10:40:00','12:50:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-05','13:30:00','15:40:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-05','16:00:00','18:10:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-05','18:30:00','20:40:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-05','21:15:00','23:25:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-06','08:00:00','10:10:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-06','10:40:00','12:50:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-06','13:30:00','15:40:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-06','16:00:00','18:10:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-06','18:30:00','20:40:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-06','21:15:00','23:25:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-07','08:00:00','10:10:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-07','10:40:00','12:50:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-07','13:30:00','15:40:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-07','16:00:00','18:10:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-07','18:30:00','20:40:00','45000','A B C D E F G H I J K L M N;10;'),
('3','3','2018-08-07','21:15:00','23:25:00','45000','A B C D E F G H I J K L M N;10;');



INSERT INTO Schedules(movie_id,room_id,show_date,start_time,end_time,price,schedule_room_seat) VALUE
('4','4','2018-08-02','08:30:00','10:05:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-02','13:15:00','14:50:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-02','14:30:00','16:05:00','45000','A B C D E F G H I J K L M N;10;'),
('4','5','2018-08-02','16:25:00','18:00:00','45000','A B C D E F G H I J;10;'),
('4','4','2018-08-02','16:45:00','18:20:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-02','19:00:00','20:35:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-02','21:00:00','22:35:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-03','08:30:00','10:05:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-03','13:15:00','14:50:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-03','14:30:00','16:05:00','45000','A B C D E F G H I J K L M N;10;'),
('4','5','2018-08-03','16:25:00','18:00:00','45000','A B C D E F G H I J;10;'),
('4','4','2018-08-03','16:45:00','18:20:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-03','19:00:00','20:35:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-03','21:00:00','22:35:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-04','08:30:00','10:05:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-04','13:15:00','14:50:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-04','14:30:00','16:05:00','45000','A B C D E F G H I J K L M N;10;'),
('4','5','2018-08-04','16:25:00','18:00:00','45000','A B C D E F G H I J;10;'),
('4','4','2018-08-04','16:45:00','18:20:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-04','19:00:00','20:35:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-04','21:00:00','22:35:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-05','08:30:00','10:05:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-05','13:15:00','14:50:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-05','14:30:00','16:05:00','45000','A B C D E F G H I J K L M N;10;'),
('4','5','2018-08-05','16:25:00','18:00:00','45000','A B C D E F G H I J;10;'),
('4','4','2018-08-05','16:45:00','18:20:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-05','19:00:00','20:35:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-05','21:00:00','22:35:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-06','08:30:00','10:05:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-06','13:15:00','14:50:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-06','14:30:00','16:05:00','45000','A B C D E F G H I J K L M N;10;'),
('4','5','2018-08-06','16:25:00','18:00:00','45000','A B C D E F G H I J;10;'),
('4','4','2018-08-06','16:45:00','18:20:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-06','19:00:00','20:35:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-06','21:00:00','22:35:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-07','08:30:00','10:05:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-07','13:15:00','14:50:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-07','14:30:00','16:05:00','45000','A B C D E F G H I J K L M N;10;'),
('4','5','2018-08-07','16:25:00','18:00:00','45000','A B C D E F G H I J;10;'),
('4','4','2018-08-07','16:45:00','18:20:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-07','19:00:00','20:35:00','45000','A B C D E F G H I J K L M N;10;'),
('4','4','2018-08-07','21:00:00','22:35:00','45000','A B C D E F G H I J K L M N;10;');

INSERT INTO Schedules(movie_id,room_id,show_date,start_time,end_time,price,schedule_room_seat) VALUE
('5','5','2018-08-02','08:40:00','10:25:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-02','10:50:00','12:35:00','45000','A B C D E F G H I J;10;'),
('5','4','2018-08-02','11:00:00','12:45:00','45000','A B C D E F G H I J K L M N;10;'),
('5','5','2018-08-02','13:10:00','14:55:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-02','18:15:00','20:00:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-02','20:15:00','21:00:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-02','21:15:00','23:00:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-03','08:40:00','10:25:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-03','10:50:00','12:35:00','45000','A B C D E F G H I J;10;'),
('5','4','2018-08-03','11:00:00','12:45:00','45000','A B C D E F G H I J K L M N;10;'),
('5','5','2018-08-03','13:10:00','14:55:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-03','18:15:00','20:00:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-03','20:15:00','21:00:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-03','21:15:00','23:00:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-04','08:40:00','10:25:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-04','10:50:00','12:35:00','45000','A B C D E F G H I J;10;'),
('5','4','2018-08-04','11:00:00','12:45:00','45000','A B C D E F G H I J K L M N;10;'),
('5','5','2018-08-04','13:10:00','14:55:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-04','18:15:00','20:00:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-04','20:15:00','21:00:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-04','21:15:00','23:00:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-05','08:40:00','10:25:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-05','10:50:00','12:35:00','45000','A B C D E F G H I J;10;'),
('5','4','2018-08-05','11:00:00','12:45:00','45000','A B C D E F G H I J K L M N;10;'),
('5','5','2018-08-05','13:10:00','14:55:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-05','18:15:00','20:00:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-05','20:15:00','21:00:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-05','21:15:00','23:00:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-06','08:40:00','10:25:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-06','10:50:00','12:35:00','45000','A B C D E F G H I J;10;'),
('5','4','2018-08-06','11:00:00','12:45:00','45000','A B C D E F G H I J K L M N;10;'),
('5','5','2018-08-06','13:10:00','14:55:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-06','18:15:00','20:00:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-06','20:15:00','21:00:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-06','21:15:00','23:00:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-07','08:40:00','10:25:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-07','10:50:00','12:35:00','45000','A B C D E F G H I J;10;'),
('5','4','2018-08-07','11:00:00','12:45:00','45000','A B C D E F G H I J K L M N;10;'),
('5','5','2018-08-07','13:10:00','14:55:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-07','18:15:00','20:00:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-07','20:15:00','21:00:00','45000','A B C D E F G H I J;10;'),
('5','5','2018-08-07','21:15:00','23:00:00','45000','A B C D E F G H I J;10;');


INSERT INTO Schedules(movie_id,room_id,show_date,start_time,end_time,price,schedule_room_seat) VALUE
('6','6','2018-08-02','08:00:00','10:00:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-02','10:30:00','12:30:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-02','13:00:00','15:00:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-02','15:30:00','17:30:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-02','18:00:00','20:00:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-02','20:30:00','22:30:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-03','08:00:00','10:00:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-03','10:30:00','12:30:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-03','13:00:00','15:00:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-03','15:30:00','17:30:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-03','18:00:00','20:00:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-03','20:30:00','22:30:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-04','08:00:00','10:00:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-04','10:30:00','12:30:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-04','13:00:00','15:00:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-04','15:30:00','17:30:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-04','18:00:00','20:00:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-04','20:30:00','22:30:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-05','08:00:00','10:00:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-05','10:30:00','12:30:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-05','13:00:00','15:00:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-05','15:30:00','17:30:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-05','18:00:00','20:00:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-05','20:30:00','22:30:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-06','08:00:00','10:00:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-06','10:30:00','12:30:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-06','13:00:00','15:00:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-06','15:30:00','17:30:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-06','18:00:00','20:00:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-06','20:30:00','22:30:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-07','08:00:00','10:00:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-07','10:30:00','12:30:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-07','13:00:00','15:00:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-07','15:30:00','17:30:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-07','18:00:00','20:00:00','45000','A B C D E F G H I J K L M N;10;'),
('6','6','2018-08-07','20:30:00','22:30:00','45000','A B C D E F G H I J K L M N;10;');


INSERT INTO Schedules(movie_id,room_id,show_date,start_time,end_time,price,schedule_room_seat) VALUE
('7','7','2018-08-02','08:00:00','10:00:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-02','10:30:00','12:30:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-02','13:00:00','15:00:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-02','15:30:00','17:30:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-02','18:00:00','20:00:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-02','20:30:00','22:30:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-03','08:00:00','10:00:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-03','10:30:00','12:30:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-03','13:00:00','15:00:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-03','15:30:00','17:30:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-03','18:00:00','20:00:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-03','20:30:00','22:30:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-04','08:00:00','10:00:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-04','10:30:00','12:30:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-04','13:00:00','15:00:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-04','15:30:00','17:30:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-04','18:00:00','20:00:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-04','20:30:00','22:30:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-05','08:00:00','10:00:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-05','10:30:00','12:30:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-05','13:00:00','15:00:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-05','15:30:00','17:30:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-05','18:00:00','20:00:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-05','20:30:00','22:30:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-06','08:00:00','10:00:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-06','10:30:00','12:30:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-06','13:00:00','15:00:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-06','15:30:00','17:30:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-06','18:00:00','20:00:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-06','20:30:00','22:30:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-07','08:00:00','10:00:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-07','10:30:00','12:30:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-07','13:00:00','15:00:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-07','15:30:00','17:30:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-07','18:00:00','20:00:00','45000','A B C D E F G H I J K L M N;10;'),
('7','7','2018-08-07','20:30:00','22:30:00','45000','A B C D E F G H I J K L M N;10;');


INSERT INTO Schedules(movie_id,room_id,show_date,start_time,end_time,price,schedule_room_seat) VALUE
('8','8','2018-08-02','09:00:00','10:25:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-02','10:40:00','12:05:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-02','13:00:00','14:25:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-02','14:50:00','16:15:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-02','16:45:00','18:10:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-02','18:30:00','19:55:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-02','19:30:00','20:55:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-02','21:10:00','22:35:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-02','22:40:00','24:00:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-03','09:00:00','10:25:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-03','10:40:00','12:05:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-03','13:00:00','14:25:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-03','14:50:00','16:15:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-03','16:45:00','18:10:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-03','18:30:00','19:55:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-03','19:30:00','20:55:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-03','21:10:00','22:35:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-03','22:40:00','24:00:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-04','09:00:00','10:25:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-04','10:40:00','12:05:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-04','13:00:00','14:25:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-04','14:50:00','16:15:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-04','16:45:00','18:10:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-04','18:30:00','19:55:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-04','19:30:00','20:55:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-04','21:10:00','22:35:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-04','22:40:00','24:00:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-05','09:00:00','10:25:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-05','10:40:00','12:05:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-05','13:00:00','14:25:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-05','14:50:00','16:15:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-05','16:45:00','18:10:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-05','18:30:00','19:55:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-05','19:30:00','20:55:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-05','21:10:00','22:35:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-05','22:40:00','24:00:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-06','09:00:00','10:25:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-06','10:40:00','12:05:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-06','13:00:00','14:25:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-06','14:50:00','16:15:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-06','16:45:00','18:10:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-06','18:30:00','19:55:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-06','19:30:00','20:55:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-06','21:10:00','22:35:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-06','22:40:00','24:00:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-07','09:00:00','10:25:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-07','10:40:00','12:05:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-07','13:00:00','14:25:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-07','14:50:00','16:15:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-07','16:45:00','18:10:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-07','18:30:00','19:55:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-07','19:30:00','20:55:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-07','21:10:00','22:35:00','45000','A B C D E F G H I J K L M N;10;'),
('8','8','2018-08-07','22:40:00','24:00:00','45000','A B C D E F G H I J K L M N;10;');







