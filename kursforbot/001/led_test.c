#include <stdio.h>
#include <dos.h>
int DDRA;
int PORTD;
unsigned char PORTA; // 8 bits // unsigned long
int PIND = 1;
#define _delay_ms(ms) sleep(ms/200)
#define BYTETOBINARYPATTERN "%d%d%d%d%d%d%d%d\n\r"
#define BYTETOBINARY(byte)  \
  (byte & 0x80 ? 1 : 0), \
  (byte & 0x40 ? 1 : 0), \
  (byte & 0x20 ? 1 : 0), \
  (byte & 0x10 ? 1 : 0), \
  (byte & 0x08 ? 1 : 0), \
  (byte & 0x04 ? 1 : 0), \
  (byte & 0x02 ? 1 : 0), \
  (byte & 0x01 ? 1 : 0) 


#define DELAY 200

enum DIRECTION
{
	NONE,
	LEFT,
	RIGHT
};

// maska bitowa
#ifndef _BV
#define _BV(bit) (1<<(bit))
#endif

// przycisk
#ifndef PD0
#define PD0 0x01
#endif

int main(void)
{
	enum DIRECTION previous_direction = NONE;
	enum DIRECTION current_direction = NONE;
	
	// ustawienie PORTA jako wyjscie
	DDRA = 0xff;
	// ustawienie wartosci 0 na PORTD, nie wiem czy potrzebne
	PORTD |= _BV(0);
	
	while(1)
	{
		// przycisk wcisniety
		if(!(PIND & _BV(PD0)))
		{
			if ((previous_direction != LEFT) || ((PORTA >> 1) == 0))
			{
				previous_direction = LEFT;
				current_direction = LEFT;
				printf("guzik wcisniety lub od nowa\n\r");
				// ustawienie wartosci 1 dla portu A
				PORTA = 0b10000000;
				printf("PORTA: "BYTETOBINARYPATTERN, BYTETOBINARY(PORTA));
				_delay_ms(DELAY);
			}
			PORTA >>= 1;
			printf("PORTA: "BYTETOBINARYPATTERN, BYTETOBINARY(PORTA));
			_delay_ms(DELAY);
		}
		else
		//if((PIND ^ _BV(PD0)))
		{
			if ((previous_direction != RIGHT) || ((PORTA << 1) > 128))
			{
				previous_direction = RIGHT;
				current_direction = RIGHT;
				printf("guzik niewcisniety lub od nowa\n\r");
				// ustawienie wartosci 1 dla portu A
				PORTA = 0b00000001;
				printf("PORTA: "BYTETOBINARYPATTERN, BYTETOBINARY(PORTA));
				_delay_ms(DELAY);
			}
			PORTA <<= 1;
			printf("PORTA: "BYTETOBINARYPATTERN, BYTETOBINARY(PORTA));
			_delay_ms(DELAY);
		}
	}
}
