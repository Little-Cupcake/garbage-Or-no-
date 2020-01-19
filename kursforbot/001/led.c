#define F_CPU 1000000L
#define DELAY 200

#include <avr/io.h>
#include <util/delay.h>                

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
#define PD0 0x00
#endif

int main(void)
{
	enum DIRECTION previous_direction = NONE;
	enum DIRECTION current_direction = NONE;
	
	// ustawienie PORTA jako wyjscie
	DDRA = 0xff;
	// ustawienie wartosci 0 na PORTD
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
				// ustawienie wartosci 1 dla portu A
				PORTA = 0b10000000;
				_delay_ms(DELAY);
			}
			PORTA >>= 1;
			_delay_ms(DELAY);
		}
		else
		//if((PIND ^ _BV(PD0)))
		{
			if ((previous_direction != RIGHT) || ((PORTA << 1) > 128))
			{
				previous_direction = RIGHT;
				current_direction = RIGHT;
				// ustawienie wartosci 1 dla portu A
				PORTA = 0b00000001;
				_delay_ms(DELAY);
			}
			PORTA <<= 1;
			_delay_ms(DELAY);
		}
	}
}
