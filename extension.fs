( STACK MANIPULATION )
: nip   >r drop r> ;            ( n1 n2 -- n2 )
: swap  over >r nip r> ;        ( n1 n2 -- n2 n1 ) 
: tuck  swap over ;             ( n1 n2 -- n2 n1 n2 )
: rot   >r swap r> swap ;       ( n1 n2 n3 -- n2 n3 n1 )
: r     r> r> dup >r swap >r ;  ( -- r )

: -rot  swap >r swap r> ;   ( n1 n2 n3 -- n3 n1 n2 )
: ?dup  if -> then dup ;    ( n -- n ? )
: 2drop drop drop ;         ( n1 n2 -- )
: 2nip  >r nip nip r> ;     ( n1 n2 n3 n4 -- n3 n4 )
: 2dup  over over ;         ( n1 n2 -- n1 n2 n1 n2 )
: 2swap >r -rot r> -rot ;   ( n1 n2 n3 n4 -- n3 n4 n1 n2 )

: 2over >r >r 2dup r> -rot r> -rot ;            ( n1 n2 n3 n4 -- n1 n2 n3 n4 n1 n2 )
: 2tuck 2dup >r >r >r -rot r> -rot r> r> ;      ( n1 n2 n3 n4 -- n3 n4 n1 n2 n3 n4 )
: third >r >r dup r> swap r> swap ;             ( n1 n2 n3 -- n1 n2 n3 n1 )
: fourth >r >r >r dup r> swap r> swap r> swap ; ( n1 n2 n3 n4 -- n1 n2 n3 n4 n1 )

( ALTERNATIVES )
( : nip >r drop r> ; )
( : nip over xor xor ; )
( : swap >r a! r> a ; )
( : tuck a! >r a r> a ; )
( : rot >r >r a! r> r> a ; )
( : r r> r> a! >r >r a ; )

( COMPARISON )
: <  - -if drop 0 -> then drop -1 ;         ( n1 n2 -- f )
: =  - if -> then drop 1 ;                  ( n1 n2 -- f )
: >  swap - -if drop 0 -> then drop -1 ;    ( n1 n2 -- f )
: 0< -if drop 0 -> then drop -1 ;           ( n -- f )
: 0= if -> then drop -1 ;                   ( n -- f )
: 0> negate -if drop 0 -> then drop -1 ;    ( n -- f )

( ARITHMETIC AND LOGICAL )
: -      inv + 1 + ;                                        ( n1 n2 -- diff )
: 1+     1 + ;                                              ( n -- n+1 )
: 1-     -1 + ;                                             ( n -- n-1 )
: 2+     2 + ;                                              ( n -- n+2 )
: 2-     -2 + ;                                             ( n -- n-2 )
: *      -if >r negate a! r> negate a swap then a! 0
         !< >r a if drop drop r> -> then drop r> +* <- ;    ( n1 n2 -- prod )
: /      a! 0 !< >r -if drop r> 1- -> then a - r> 1+ <- ;   ( n1 n2 -- quot )
: mod    swap !< over - -if + -> then <- ;                  ( n1 n2 -- rem )
: /mod   over over mod rot rot / ;                          ( n1 n2 -- rem quot )
: */mod  >r * r> /mod ;                                     ( n1 n2 n3 -- rem quot )
: */     >r * r> / ;                                        ( n1 n2 n3 -- quot )
: max    over over < if drop nip -> then drop drop ;        ( n1 n2 -- max )
: min    over over > if drop nip -> then drop drop ;        ( n1 n2 -- min )
: abs    -if inv 1 + ;                                      ( n -- |n| )
: negate inv 1 + ;                                          ( n -- -n )
: or     over over and >r xor r> + ;                        ( n1 n2 -- or )

( : mod a! !< dup >r a - -if drop r> -> then r> drop <- ; )

( MEMORY )
: !-    ! a -1 + a! ;                                       ( n -- )
: @-    @ a -1 + a! ;                                       ( -- n )
: ?     @ . ;                                               ( -- )
: +!    @ + ! ;                                             ( n -- )
: move  1- for swap a! a 1+ swap @ swap a! ! a 1+ next ;    ( addr dest u -- )
: fill  >r if drop r> drop -> then 1- r> dup !+ <- ;        ( u byte -- )
: erase 1- for 0 !+ next ;                                  ( u -- )
: dump  for cr 1 b! a !b 58 emit space @+ . next ;          ( u -- )

( CONTROL STRUCTURES )
: exit r> drop -2 >r ; ( -- )

( TERMINAL INPUT-OUTPUT )
: .      1 b! !b 0 b! 32 !b ;               ( n -- )
: emit   0 b! !b ;                          ( n -- )
: cr     0 b! 10 !b ;                       ( -- )
: space  0 b! 32 !b ;                       ( -- )
: spaces 0 b! for 160 !b next ;             ( u -- )
: page   0 b! 0 !b ;                        ( -- )
: type   if drop -> then 1- @+ emit <- ;    ( u -- )
: .r     0 b! for 160 !b next . ;           ( n u -- )
: .s     10 a!  8 for !+ next !
         8 for @- . next @ .
         19 a!  8 for @- next @ ;           ( -- )

( COLORS )
: black         0 ;  ( -- n )
: white         1 ;  ( -- n )
: red           2 ;  ( -- n )
: cyan          3 ;  ( -- n )
: violet        4 ;  ( -- n )
: green         5 ;  ( -- n )
: blue          6 ;  ( -- n )
: yellow        7 ;  ( -- n )
: orange        8 ;  ( -- n )
: brown         9 ;  ( -- n )
: light-red     10 ; ( -- n )
: dark-gray     11 ; ( -- n )
: gray          12 ; ( -- n )
: light-green   13 ; ( -- n )
: light-blue    14 ; ( -- n )
: light-gray    15 ; ( -- n )

: fg-color 2 b! !b ; ( n -- )
: bg-color 3 b! !b ; ( n -- )

black fg-color
white bg-color

: intro
( ForthScript 0.1.0, Open Source (2024) by T. Szulc )
70 emit 111 emit 114 emit 116 emit 104 emit 83 emit 99 emit 114 emit 105 emit
112 emit 116 emit 32 emit 48 emit 46 emit 49 emit 46 emit 49 emit 44 emit 32
emit 79 emit 112 emit 101 emit 110 emit 32 emit 83 emit 111 emit 117 emit 114
emit 99 emit 101 emit 32 emit 40 emit 50 emit 48 emit 50 emit 52 emit 41 emit
32 emit 98 emit 121 emit 32 emit 84 emit 46 emit 32 emit 83 emit 122 emit 117
emit 108 emit 99 emit 10 emit
; intro

: colors
( addr[2] )
( 0: black, 1: white, 2: red, 3: cyan, 4: violet, 5: green, 6: blue )
( 7: yellow, 8: orange, 9: brown, 10: light red, 11: dark grey, 12: gray )
( 13: light green, 14: light blue, 15: light grey )
10 emit 102 emit 111 emit 114 emit 101 emit 103 emit 114 emit 111 emit 117 emit
110 emit 100 emit 32 emit 61 emit 32 emit 97 emit 100 emit 100 emit 114 emit 91
emit 50 emit 93 emit 10 emit 98 emit 97 emit 99 emit 107 emit 103 emit 114 emit
111 emit 117 emit 110 emit 100 emit 32 emit 61 emit 32 emit 97 emit 100 emit
100 emit 114 emit 91 emit 51 emit 93 emit 10 emit 48 emit 58 emit 32 emit 98
emit 108 emit 97 emit 99 emit 107 emit 10 emit 49 emit 58 emit 32 emit 119 emit
104 emit 105 emit 116 emit 101 emit 10 emit 50 emit 58 emit 32 emit 114 emit
101 emit 100 emit 10 emit 51 emit 58 emit 32 emit 99 emit 121 emit 97 emit 110
emit 10 emit 52 emit 58 emit 32 emit 118 emit 105 emit 111 emit 108 emit 101
emit 116 emit 10 emit 53 emit 58 emit 32 emit 103 emit 114 emit 101 emit 101
emit 110 emit 10 emit 54 emit 58 emit 32 emit 98 emit 108 emit 117 emit 101
emit 10 emit 55 emit 58 emit 32 emit 121 emit 101 emit 108 emit 108 emit 111
emit 119 emit 10 emit 56 emit 58 emit 32 emit 111 emit 114 emit 97 emit 110
emit 103 emit 101 emit 10 emit 57 emit 58 emit 32 emit 98 emit 114 emit 111
emit 119 emit 110 emit 10 emit 49 emit 48 emit 58 emit 32 emit 108 emit 105
emit 103 emit 104 emit 116 emit 32 emit 114 emit 101 emit 100 emit 10 emit 49
emit 49 emit 58 emit 32 emit 100 emit 97 emit 114 emit 107 emit 32 emit 103
emit 114 emit 101 emit 121 emit 10 emit 49 emit 50 emit 58 emit 32 emit 103
emit 114 emit 97 emit 121 emit 10 emit 49 emit 51 emit 58 emit 32 emit 108
emit 105 emit 103 emit 104 emit 116 emit 32 emit 103 emit 114 emit 101 emit 101
emit 110 emit 10 emit 49 emit 52 emit 58 emit 32 emit 108 emit 105 emit 103
emit 104 emit 116 emit 32 emit 98 emit 108 emit 117 emit 101 emit 10 emit 49
emit 53 emit 58 emit 32 emit 108 emit 105 emit 103 emit 104 emit 116 emit 32
emit 103 emit 114 emit 101 emit 121 emit 32 emit
;
