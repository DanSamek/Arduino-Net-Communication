#include <LiquidCrystal.h>

void setup() {
  Serial.begin(9600);
}
void loop() {
  if(Serial.available()){
    String *data;
    String res;
    String x = Serial.readString();
    data = StringSerialToArr(x);
    res = Execute(data[0],data);
    Serial.println(res);
  }
}

String turnOnLed(int pin, int value){
   pinMode(pin, OUTPUT);
   digitalWrite(pin, value);
   return "ok";
}

String showAlphabet(int RS, int Enable, int DB4, int DB5, int DB6,int DB7){
  LiquidCrystal lcd(RS,Enable,DB4,DB5,DB6,DB7);
  lcd.clear();
  for(int startChar = 65; startChar < 91; startChar++){
      lcd.print((char)startChar);
      delay(300);
      lcd.clear();    
  }
  return "ok";
}
String showCustomSymbol(int RS, int Enable, int DB4, int DB5, int DB6,int DB7){
  LiquidCrystal lcd(RS,Enable,DB4,DB5,DB6,DB7);
  lcd.begin(16,2);
  byte customChar[] = {
    B00000,
    B01110,
    B01110,
    B11111,
    B11111,
    B01110,
    B01110,
    B00000
  };
  lcd.createChar((int)0, customChar);
  lcd.home();
  lcd.write((int)0);
  delay(500);
  lcd.clear();
  return "ok";
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
  //ALL FUNCTIONS WILL GO HERE
  String res;
  if(function == "turnOnLed") res = turnOnLed(data[1].toInt(), data[2].toInt());
  if(function == "showAlphabet") res = showAlphabet(data[1].toInt(), data[2].toInt(), data[3].toInt(),data[4].toInt(),data[5].toInt(), data[6].toInt());
  if(function == "showCustomSymbol") res = showCustomSymbol(data[1].toInt(), data[2].toInt(), data[3].toInt(),data[4].toInt(),data[5].toInt(), data[6].toInt());
  return res;
}
