      HƯỚNG DẪN SỬ DỤNG
Các bước để có thể cài đặt và trải nghiệm cờ vua:
Bước 1: Tải thư mục Project tên là “GAME CƠ VUA” về máy tính và giải nén tại vị trí mong muốn.
Bước 2: Vào nền tảng IDE Visual Studio và click “Open a project or solution”.
Bước 3: Khi cửa sổ của File Explorer hiện ra, hãy chọn đến vị trí mà ta vừa giải nén và chọn vào folder đó.
Bước 4: Trong folder “GAME CỜ VUA” hãy chọn file “Chess.sln” và click “Open”.
Bước 5: Khi đã mở thành công được project, trong phần Solution Explorer sẽ hiển thị lên bố cục của project của dự án.
Bước 6: Khi muốn chạy chương trình, ta chỉ cần nhấn tổ hợp phím Ctrl +F5 hoặc Ctrl + Fn +F5
Bước 7: Khi chương trình chạy thành công, cửa sổ nhập tên người chơi hiển thị và người chơi nhập tên, chọn mốc thời gian chơi và xác nhận. Khi click “Xác nhận” thì màn hình ván cờ hiển thị, ván cờ bắt đầu.
CHÚ Ý: Nếu người cài đặt trò chơi muốn sử dụng tiếng di chuyển của quân cờ, thì trong file MainWindow.xaml.cs và tìm đến phương thức PlayMoveSound(). Trong phương thức này, hãy sửa biến string soundPath bằng đường dẫn tuyệt đối đến với file sound_move.wav (trong thư mục ChessUI\Assets).
