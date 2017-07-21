#include <iostream>
#include <fstream>
#include <vector>
using namespace std;

int state = 0;
vector<char> stack;


void Failure() {
	state = -1;
}

//process states
int process(char input) {
	switch (state) {
	case 0:
		switch (input) {
		case 'i':
			if (stack.at(stack.size() - 1) == 'z') {
				state = 1;
			}
			else if (stack.at(stack.size() - 1) == '{') {
				state = 1;
			}
			else {
				Failure();
			}
			break;
		case '}':
			if (stack.at(stack.size() - 1) == '{') {
				stack.pop_back();
				state = 2;
			}
			else {
				Failure();
			}
			break;
		case '\r':

			break;
		case '\n':
			state = 100;
			break;
		case '\0':
			state = 100;
			break;
		case -1:
			state = 100;
			break;
		default:
			Failure();
			return 1;
			break;
		}
		break;
	case 1:
		switch (input) {
		case 'f':
			if (stack.at(stack.size() - 1) == 'z') {
				state = 2;
			}
			else if (stack.at(stack.size() - 1) == '{') {
				state = 2;
			}
			else {
				Failure();
			}
			break;
		default:
			state = -1;
			return 1;
			break;
		}
		break;
	case 2:
		switch (input) {
		case 'i':
			if (stack.at(stack.size() - 1) == 'z') {
				state = 1;
			}
			else if (stack.at(stack.size() - 1) == '{') {
				state = 1;
			}
			else {
				Failure();
			}
			break;
		case 'e':
			if (stack.at(stack.size() - 1) == 'z') {
				state = 3;
			}
			else if (stack.at(stack.size() - 1) == '{') {
				state = 3;
			}
			else {
				Failure();
			}
			break;
		case '{':
			if (stack.at(stack.size() - 1) == 'z') {
				stack.push_back('{');
				state = 0;
			}
			else {
				Failure();
			}
			break;
		case '}':
			 if (stack.at(stack.size() - 1) == '{') {
				stack.pop_back();
				state = 2;
			}
			else {
				Failure();
			}
			break;
		case '\r':
				
			break;
		case '\n':
			state = 100;
			break;
		case '\0':
			state = 100;
			break;
		case ' ':
			state = 100;
			break;
		case -1:
			state = 100;
			break;
		default:
			Failure();
			return 1;
			break;
		}
		break;
	case 3:
		switch (input) {
		case 'l':
			if (stack.at(stack.size() - 1) == 'z') {
				state = 4;
			}
			else if (stack.at(stack.size() - 1) == '{') {
				state = 4;
			}
			else {
				Failure();
			}
			break;
		default:
			Failure();
			return 1;
			break;
		}
		break;
	case 4:
		switch (input) {
		case 's':
			if (stack.at(stack.size() - 1) == 'z') {
				state = 5;
			}
			else if (stack.at(stack.size() - 1) == '{') {
				state = 5;
			}
			else {
				Failure();
			}
			break;
		default:
			Failure();
			return 1;
			break;
		}
		break;
	case 5:
		switch (input) {
		case 'e':
			if (stack.at(stack.size() - 1) == 'z') {
				state = 0;
			}
			else if (stack.at(stack.size() - 1) == '{') {
				state = 0;
			}
			else {
				Failure();
			}
			break;
		default:
			Failure();
			return 1;
			break;
		}
		break;
	case -1:
		switch (input) {
		case '\n':
			state = 101;
			break;
		case -1:
			state = 101;
			break;
		}
		break;
	default:
		state = -1;
		return 1;
		break;
	}

	return 0;
}

//next input set
void Reset() {
	stack.clear();
	stack.push_back('z');
	state = 0;
}

int main()
{
	Reset();

	std::cout << "If Else Parsing" << endl;

	ifstream infile("sequence.txt");
	if (infile.is_open()) {
		while (!infile.eof()) {

			char read = infile.get();
			//cout << read;
			cout << "(q" << state << "," << read << "," << stack[stack.size() - 1] << ")-->";
			process(read); //process for transition
			cout << "(q" << state << "," << read << "," << stack[stack.size() - 1] << ")" << endl;

			if (state == 100) {
				cout << "Correct Sequence" << endl;
				Reset(); //start the next one
			}
			else if(state == 101) {
				cout << "Sequence is syntactically incorrect" << endl;
				Reset(); //start the next one
			}
			

		}
	}
	infile.close();

	return 0;

}