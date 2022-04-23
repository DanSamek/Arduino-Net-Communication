#include <mainFunctions.h>

void setup() {
  Serial.begin(9600);
}
void loop() {
  if(Serial.available()){
    String fn = Read(Serial.ReadString());;
    String res = Execute(fn);
    Serial.Write(res);
  }
}
