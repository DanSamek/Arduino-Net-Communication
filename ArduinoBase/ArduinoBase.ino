
void setup() {
  Serial.begin(9600);
}

String *StringSerialToArr(String data) {
  static String splittedData[20];
  String between = "";
  const char *separator = ";";
  int index=0;
  for(int b = 0; b< data.length(); b++){ 
    if(data[b] != *separator){
      between += data[b];
    } else {
      splittedData[index] = between;
      index++;
      between ="";
    }
    if(b+1 == data.length()){
      splittedData[index] = between;
    }
  }
  return splittedData;
}


String Execute(String function, String data[]){
  //TODO
  
}

void loop() {
  if(Serial.available()){
    String *data;
    data = StringSerialToArr(Serial.readString());
    String res = Execute(data[0],data);
  }
}

