#include "setupFile.h"

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
  if(function == "turnOnLed") turnOnLed(data[1].toInt(), data[2].toInt());
}
