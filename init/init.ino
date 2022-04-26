#include "setupFile.h"

void setup() {
  Serial.begin(9600);
}

void loop() {
  if(Serial.available()){
    String *data;
    String res;
    data = StringSerialToArr(Serial.readString());
    res = Execute(data[0],data);
    Serial.print(res);
  }
}
