#include <ncurses.h>

void NoEcho()
{
    noecho();
}

void InitScr()
{
    initscr();
}

void EndWin()
{
    endwin();
}

void HalfDelay(int delay)
{
    halfdelay(delay);
}

int WGetch()
{
    return wgetch(stdscr);
}