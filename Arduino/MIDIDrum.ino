//Tamaño del array de lectura del piezoelectrico
const int numberOfReads = 50;
//Numero de entradas a leer
const int nPiezos = 6;
//Si la entrada es menor a este numero la obviamos
int threesholds[nPiezos] = {90,35,35,25,35,25};
//A partir de esta cifra consideramos que es a maximo volumen
int maxThreeshold[nPiezos] = {450,500,500,150,350,150};

//Entradsa del arduino a leer y numero de lecturas que se lleva sobre cada piezo
// 0-> Platillo derecho, 1-> Tambor derecho, 2-> Tambor Central, 3-> Platillo derecho, 4-> Tambor izquierdo, 5-> Pedal
int inputs[nPiezos] = {A0,A1,A3,A4,A5,A2};
int nReads[nPiezos] = {0,0,0,0,0,0};

//Variables para detectar golpes y guardar la nota tocada
int inputNotes[nPiezos][numberOfReads];
bool isReading[nPiezos] = {false,false,false,false,false};
int readings[nPiezos] = {0,0,0,0,0,0};
int peaks[nPiezos] = {0,0,0,0,0,0};
int notesFired[nPiezos] = {0,0,0,0,0};

//Variables para el control de las notas "dobles"
unsigned long momentFired[nPiezos] = {0,0,0,0,0,0};
unsigned long currentTime[nPiezos] = {0,0,0,0,0,0};
bool hasFirstRead[nPiezos] = {false,false,false,false,false,false};
int minDif = 100;
int minDifPedal = 300;

//Depuracion para lo platillos
/*const int nPiezos = 2;
int inputs[nPiezos] = {A0,A4};
int nReads[nPiezos] = {0,0};
int threesholds[nPiezos] = {14,14};
int maxThreeshold[nPiezos] = {70,40};
int inputNotes[nPiezos][numberOfReads];
bool isReading[nPiezos] = {false,false};
int readings[nPiezos] = {0,0};
int peaks[nPiezos] = {0,0};
int notesFired[nPiezos] = {0,0};
unsigned long momentFired[nPiezos] = {0,0};
unsigned long currentTime[nPiezos] = {0,0};
bool hasFirstRead[nPiezos] = {false,false};*/

 void setup () {
   // pone su código de instalación aquí, para ejecutarlo una vez:
   for(int i = 0; i < nPiezos; i++){
    pinMode(inputs[i],INPUT);
   }

   //Puerto donde se escriben las salidas del Arduino
   Serial.begin (9600);
 }    

 void loop () {
   // pone su código principal aquí, para ejecutar repetidamente:
   for(int i = 0; i < nPiezos; i++){
    //Leemos la entrada de cada piezo
    readings[i] = analogRead(inputs[i]);
    //Sacamos el milisegundo de la lectura
    currentTime[i] = millis();

    //Utilizamos una variable distinta de tiempo de espera entre lecturas si es el pedal o no
    int timDiff = 0;
    if(i == (nPiezos - 1)){
      timDiff = minDifPedal;
    }
    else{
      timDiff = minDif;
    }
    
    //Si la entrada es mayor al minimo la guardamos
    if(readings[i] > threesholds[i]){
      //Si esta leyendo valors de esa entrada, cogemos los valores directamente. Si no esta leyendo y va a empezar,solo guardamos los valores si ha pasado el suficiente tiempo o es la primera lectura
      if(isReading[i] || (!hasFirstRead[i] || (momentFired[i] + timDiff) <= currentTime[i])){
        //Si es la primera entrada reiniciamos el contador y limpiamos el array de lectura
        if(isReading[i] == false){
          isReading[i] = true;
          nReads[i] = 0;
          clearArray(inputNotes[i]);
        }
  
        //Si esta ha sido su primera lectura, marcamos que ya no lo es para que tenga en cuanta el tiempo
        if(!hasFirstRead[i]){
          hasFirstRead[i] = true;
        }

        /*Serial.println("-----------------------------");
        Serial.println(i);
        Serial.println(readings[i]);
        Serial.println("********************");
        Serial.print(momentFired[i]);
        Serial.print("-");
        Serial.println(currentTime[i]);*/
        
        inputNotes[i][nReads[i]] = readings[i];
        nReads[i]++;
      }
    }
    //Si la entrada es 0 terminamos de leer el golpe
    else if(readings[i] <= threesholds[i]){
      //Si ya estabamos leyendo este piezo calculamos la lectura maxima y disparamos su nota con esa velocidad
      if(isReading[i] == true){
        //Guardamos el milisegundo donde se registro la nota
        momentFired[i] = millis();
        
        isReading[i] = false;
        //Obtenemos el pico del golpe
        peaks[i] = maxValue(inputNotes[i]);
        //Nos aseguramos que la nota ete entre los limites
        peaks[i] = constrain(peaks[i],threesholds[i],maxThreeshold[i]);
        //Mapeamos el valor entre 1-127 ya que esos son lo valores la velocidad de una nota MIDI
        notesFired[i] = map(peaks[i],threesholds[i],maxThreeshold[i],1,127);
        /*Serial.println("-----------------------------");
        Serial.println(i);
        Serial.println("********************");
        Serial.print(peaks[i]);
        Serial.print("-");
        Serial.println(notesFired[i]);*/
        //Escribimos por el puerto serie los datos del sensor golpeado
        Serial.print(i);
        Serial.print("-");
        Serial.print(notesFired[i]);
        Serial.println("|");
      }
    }
   }
 }

//Devuelve el valor maximo de un array
 int maxValue(int readings[]){
  int maxValue = 0;
  for(int i = 0; i < numberOfReads; i++){
    if(readings[i] > maxValue){
      maxValue = readings[i];
    }
  }
  return maxValue;
 }

//Pone a 0 todas las entradas de un array de int
 void clearArray(int readings[]){
  for(int i = 0; i < numberOfReads; i++){
    readings[i] = 0;
  }
 }
