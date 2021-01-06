http-server ./UI &
./BackEnd/BackEnd.exe &
start chrome http://localhost:8080
google-chrome http://localhost:8080